﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core
{
    public class Result<T>
    {
        public T Data { get; set; }
        public string ErrorText { get; set; }
        public StatusCode ErrorCode { get; set; }
        public bool Success => ErrorCode == StatusCode.OK;

        public static Result<T> CreateSuccessful(T data)
        {
            return new Result<T>
            {
                ErrorCode = StatusCode.OK,
                Data = data
            };
        }

        public static Result<T> CreateFailed(StatusCode code,
            string text)
        {
            return new Result<T>
            {
                ErrorCode = code,
                ErrorText = text
            };
        }

   }
}
