using Remotion.Mixins.Definitions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net;
using Web.MainApplication.Controllers;
using System.Web.Helpers;
using Newtonsoft.Json;
using Web.MainApplication.Models.ApiModel;

namespace Web.MainApplication.Service.ApiHelper
{
    public class RestReposityiHuman
    {
        private readonly RestClient _client;
        private readonly string _url = ConfigurationManager.AppSettings["iHumanBaseURL"];

        public RestReposityiHuman()
        {
            _client = new RestClient(_url);
        }
        private void GetToken(BaseController controller)
        {
            if (controller.Session["hrisaccess_token"] == null)
            {

                _client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("grant_type", "password");
                request.AddParameter("username", "Inventory");
                request.AddParameter("password", "Inventory");
                IRestResponse response = _client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var tokenResult = JsonConvert.DeserializeObject<TokenContainer>(response.Content);
                    controller.Session["hrisaccess_token"] = tokenResult.access_token;
                    controller.Session["hrisaccess_token_expires_in"] = DateTime.Now.AddSeconds(tokenResult.expires_in);
                }
                Console.WriteLine(response.Content);

            }
            else
            {
                if ()
            }
        }
        //public IEnumerable<serverdatamodel> GetAll()
        //{
        //    var request = new RestRequest
        //    ("api/serverdata", Method.GET)
        //    { RequestFormat = DataFormat.Json };

        //    var response = _client.Execute<List<serverdatamodel>>(request);

        //    if (response.Data == null)
        //        throw new Exception(response.ErrorMessage);

        //    return response.Data;
        //}


    }
}