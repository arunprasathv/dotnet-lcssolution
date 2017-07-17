using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCSExercise.Helper
{
    public static class HttpResponseText
    {
        public const string UnknownErrorOccured = "Unknown error has occurred.";
        public const string ValueCannotBeEmpty = "Values cannot be empty.Provide a Valid JSON in the Request Body.";
        public const string NoUniqueStrings = "All Strings must be Unique.";
        public const string RequestJSONInvalid = "Request JSON body is invalid";
        public const string ErrorProcessingRequest = "An error occurred while processing the request.";
    }
}
