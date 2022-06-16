import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Response } from '@data/schema/response.model';
import { UserInsert } from '@data/schema/user-insert.model';
import { User } from '@data/schema/user.model';
import { UserService } from '@data/service/user.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  error!: string;
  isLoading: boolean = false;
  formlogin!: FormGroup;
  hidePasswordIcon: boolean = true;

  get f() {
    return this.formlogin.controls;
  }

  private _subscription: Subscription = new Subscription();

  constructor(
    private _formBuilder: FormBuilder,
    private _router: Router,
    private _userService: UserService,
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

    if (!this.formlogin.valid) {
      this.isLoading = false;
      return;
    }

    let userInsert: UserInsert = {
       Username : this.f['username'].value,
       Password : this.f['password'].value,
       Lastname : this.f['lastname'].value,
       Firstname : this.f['firstname'].value
    };

    this._subscription = this._userService
      .register(userInsert)
      .subscribe({
        next: (userResponse: Response<User>) => this.handleRegisterResponse(userResponse),
        error: (err: Error) => {
          this._matSnackBar.open(err.message, 'Ok');
          this.isLoading = false;
        }
      });
  }

  handleRegisterResponse(userResponse: Response<User>) {
    console.log(userResponse);
    this.isLoading = false;

    if (userResponse) {
      this._matSnackBar.open('Please login to continue.', 'Ok');
      this.goToRoute();
    }
  }

  private goToRoute() {
    this._router.navigate(['/auth/login']);
  }

  private buildForm(): void {
    this.formlogin = this._formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
    });
  }
}
