﻿namespace ProductFetcher.Dto.Service;

public class ServiceResponse<T>
{
    public bool Status { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}