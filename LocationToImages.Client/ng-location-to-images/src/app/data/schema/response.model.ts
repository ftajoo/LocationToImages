export interface Response<T> {
  Data: T;
  Succeeded: boolean;
  Error: string;
}
