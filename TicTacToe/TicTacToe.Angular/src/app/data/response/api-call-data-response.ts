import { ApiCallResponse } from "./api-call-response";

export interface ApiCallDataResponse<TDto> extends ApiCallResponse {
  dto: TDto;
}
