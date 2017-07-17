using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Net.Http.Formatting;
using System;
using System.Linq;
using LCSExercise.Helper;
using LCSExercise.LCSLogic;
using LCSExercise.Model;

namespace LCSExercise
{
    /// <summary>
    /// Controller to Find Longest Common Substring
    /// </summary>
    public class LCSController : ApiController
    {
        // GET api/LCS 
        public string Get()
        {
            return "Server is Active";
        }
        /// <summary>
        /// This API will return Longest Common Substgring for the given set of strings with O(n*m2) time complexity and satifies all the given requirements
        /// **************
        /// Logic - One by one consider all substrings of the shortest string from the given strings and for every substring check if it is a substring/whole string on the list of strings other than the shortest string
        /// 
        /// *******
        /// Supports Content Negotiation - JSON/XML.
        /// </summary>
        /// <param name="lcsRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage FindLCS([FromBody] LCSRequest lcsRequest)
        {
            var response = Request.CreateResponse();

            try
            {
                if (lcsRequest != null)
                {
                    var strList = new List<string>();
                    foreach (var str in lcsRequest.values)
                    {
                        strList.Add(str.value);
                    }
                    // To check if whole JSON or value is Null
                    if (strList.Count == 0 || strList.Any(v => (v == null || string.IsNullOrWhiteSpace(v.ToString()))))
                    {
                        response.Content = new StringContent(HttpResponseText.ValueCannotBeEmpty);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        return response;
                    }
                    // To check if there are any duplicates in the request
                    var duplicate = strList.GroupBy(s => s).SelectMany(grp => grp.Skip(1));
                    if (duplicate.Count() >= 1)
                    {
                        response.Content = new StringContent(HttpResponseText.NoUniqueStrings);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        return response;
                    }
                    //Content Negotiation Implementation
                    IContentNegotiator negotiator = this.Configuration.Services.GetContentNegotiator();
                    ContentNegotiationResult result = negotiator.Negotiate(
                        typeof(LCSResponse), this.Request, this.Configuration.Formatters);
                    if (result == null)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
                        throw new HttpResponseException(resp);
                    }
                    var lcsObject = new LCS();
                    //Assign string set
                    lcsObject.Values = strList.ToArray();
                    return new HttpResponseMessage()
                    {
                        Content = new ObjectContent<LCSResponse>(
                            lcsObject.FindLCS(),		        // What we are serializing 
                            result.Formatter,           // The media formatter
                            result.MediaType.MediaType  // The Media type
                        )
                    };
                }
                response.Content = new StringContent(HttpResponseText.RequestJSONInvalid);
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            catch (Exception ex)
            {
                response.Content = new StringContent(HttpResponseText.ErrorProcessingRequest);
                response.StatusCode = HttpStatusCode.Forbidden;
                return response;
            }
        }
        /// <summary>
        /// This API will  only work for two strings  with O(n2) time compexity.
        /// Logic - Check every character from the first string against second string , when the match is found increment the LCS counter with the previous value to know the length of the LCS string
        /// , another variable will be ovirredn with the index of the LCS character to know where the last LCS character ends.
        /// </summary>
        /// <param name="lcsRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage DynamicLCS([FromBody] LCSRequest lcsRequest)
        {
            var response = Request.CreateResponse();
            try
            {
                if (lcsRequest != null)
                {
                    string str1 = (lcsRequest.values.First()).value;
                    string str2 = (lcsRequest.values.Last()).value;
                    var lcs=LCS.FindLongestCommonSubstring(str1.ToCharArray(), str2.ToCharArray());
                    response.Content = new StringContent(lcs);
                    response.StatusCode = HttpStatusCode.OK;
                    return response;
                }
                response.Content = new StringContent(HttpResponseText.RequestJSONInvalid);
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            catch (Exception ex)
            {
                response.Content = new StringContent(HttpResponseText.ErrorProcessingRequest);
                response.StatusCode = HttpStatusCode.Forbidden;
                return response;
            }
        }
    }
}
