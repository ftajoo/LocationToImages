import { User } from "./user.model";

export interface Token {
  User: User;
  Expires: number;
  JwtToken: string;
}
