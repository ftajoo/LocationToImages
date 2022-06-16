import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AuthService } from '@core/service/auth.service';
import { Response } from '@data/schema/response.model';
import { Token } from '@data/schema/token.modet';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  error!: string;
  isLoading: boolean = false;
  formLogin!: FormGroup;
  hidePasswordIcon: boolean = true;

  get f() {
    return this.formLogin.controls;
  }

  private _subscription: Subscription = new Subscription();

  constructor(
    private _formBuilder: FormBuilder,
    private _router: Router,
    private _authService: AuthService,
    private _matSnackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.buildForm();
  }

  ngOnDestroy(): void {
    this._subscription.unsubscribe();
  }

  onSubmit() {
    this.isLoading = true;

    if (!this.formLogin.valid) {
      this.isLoading = false;
      return;
    }

    let username = this.f['username'].value;
    let password = this.f['password'].value;

    this._subscription = this._authService
      .login(username, password)
      .subscribe({
        next: (jwtResponse: Response<Token>) => this.handleLoginResponse(jwtResponse),
        error: (err: Error) => {
          this._matSnackBar.open(err.message, 'Ok');
          this.isLoading = false;
        }
      });
  }

  handleLoginResponse(jwtResponse: Response<Token>) {
    this._authService.setSession(jwtResponse);
    this.isLoading = false;

    if (jwtResponse) {
      this.goToRoute();
    }
  }

  private goToRoute() {
    this._router.navigate(['/home']);
  }

  private buildForm(): void {
    this.formLogin = this._formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }
}
