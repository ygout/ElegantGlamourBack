export interface IApiResponse<T> {
  statusCode: number;
  message: string;
  result: T;
}
