using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Servico
    {
        public async Task IniciarAsync()
        {
            Teste();

        }

        void Teste()
        {
            var list = new List<string>();

            using (var reader = new StreamReader(@"C:\Users\GuilhermeMartins\SESIRR\SESIRR.txt"))
            {
                bool first = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (!first)
                    {
                        //var obj = new List<string>();

                        // var values = line.Split(",");

                        //obj.id = values[0];

                        list.Add(line);
                    }

                    first = false;
                }
            }

            List<string> result = new List<string>();
            Parallel.ForEach(list, new ParallelOptions { MaxDegreeOfParallelism = 10 },
            item =>
            {
                try
                {
                    var accessToken = "eyJ0eXAiOiJKV1QiLCJub25jZSI6IkQxSjVrQ1lsa0djM19RWllLbkpEMWJ4Qi1RV2hnZldGYUJqd1hzTTRYWkEiLCJhbGciOiJSUzI1NiIsIng1dCI6IkN0VHVoTUptRDVNN0RMZHpEMnYyeDNRS1NSWSIsImtpZCI6IkN0VHVoTUptRDVNN0RMZHpEMnYyeDNRS1NSWSJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC80MDYxZTcxYi0yMjJmLTRkZWUtYjlmYy0wZTQxOGIyMjJlMjUvIiwiaWF0IjoxNTkwNDIzODA1LCJuYmYiOjE1OTA0MjM4MDUsImV4cCI6MTU5MDQyNzcwNSwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFTUUEyLzhQQUFBQXlDNng2TmEyb2lhSXlwZk5oNFBZRnk3U3FHY1dSLzU3R21QaFk3dm5yTXM9IiwiYW1yIjpbInB3ZCJdLCJhcHBfZGlzcGxheW5hbWUiOiJHcmFwaCBleHBsb3JlciIsImFwcGlkIjoiZGU4YmM4YjUtZDlmOS00OGIxLWE4YWQtYjc0OGRhNzI1MDY0IiwiYXBwaWRhY3IiOiIwIiwiZmFtaWx5X25hbWUiOiIzNjUiLCJnaXZlbl9uYW1lIjoiQWRtaW4iLCJpcGFkZHIiOiIxNjguMTgxLjY5LjEyNSIsIm5hbWUiOiJBZG1pbiAzNjUiLCJvaWQiOiIwNzFlNmI2NC02NjAzLTQ2ODYtYjU5MC0zZGNhNDZmNGZmYmIiLCJwbGF0ZiI6IjMiLCJwdWlkIjoiMTAwMzIwMDAzOEU5RkYyMSIsInNjcCI6IkNhbGVuZGFycy5SZWFkV3JpdGUgQ29udGFjdHMuUmVhZFdyaXRlIERldmljZU1hbmFnZW1lbnRBcHBzLlJlYWRXcml0ZS5BbGwgRGV2aWNlTWFuYWdlbWVudENvbmZpZ3VyYXRpb24uUmVhZC5BbGwgRGV2aWNlTWFuYWdlbWVudENvbmZpZ3VyYXRpb24uUmVhZFdyaXRlLkFsbCBEZXZpY2VNYW5hZ2VtZW50TWFuYWdlZERldmljZXMuUHJpdmlsZWdlZE9wZXJhdGlvbnMuQWxsIERldmljZU1hbmFnZW1lbnRNYW5hZ2VkRGV2aWNlcy5SZWFkLkFsbCBEZXZpY2VNYW5hZ2VtZW50UkJBQy5SZWFkLkFsbCBEZXZpY2VNYW5hZ2VtZW50UkJBQy5SZWFkV3JpdGUuQWxsIERldmljZU1hbmFnZW1lbnRTZXJ2aWNlQ29uZmlnLlJlYWQuQWxsIERpcmVjdG9yeS5BY2Nlc3NBc1VzZXIuQWxsIERpcmVjdG9yeS5SZWFkV3JpdGUuQWxsIEZpbGVzLlJlYWRXcml0ZS5BbGwgR3JvdXAuUmVhZFdyaXRlLkFsbCBJZGVudGl0eVJpc2tFdmVudC5SZWFkLkFsbCBNYWlsLlJlYWRXcml0ZSBNYWlsYm94U2V0dGluZ3MuUmVhZFdyaXRlIE5vdGVzLlJlYWRXcml0ZS5BbGwgb3BlbmlkIFBlb3BsZS5SZWFkIFByZXNlbmNlLlJlYWQgUHJlc2VuY2UuUmVhZC5BbGwgcHJvZmlsZSBSZXBvcnRzLlJlYWQuQWxsIFNpdGVzLlJlYWRXcml0ZS5BbGwgVGFza3MuUmVhZFdyaXRlIFVzZXIuUmVhZCBVc2VyLlJlYWRCYXNpYy5BbGwgVXNlci5SZWFkV3JpdGUgVXNlci5SZWFkV3JpdGUuQWxsIGVtYWlsIiwic3ViIjoiMVVqUU1aS3N0RTRKbW52V1ZMbFRVSFRCQjJQOXJZWnRDaXR5RUFPUFNFZyIsInRpZCI6IjQwNjFlNzFiLTIyMmYtNGRlZS1iOWZjLTBlNDE4YjIyMmUyNSIsInVuaXF1ZV9uYW1lIjoiYWRtaW4zNjVAc2VzaWVkdXJyLm9ubWljcm9zb2Z0LmNvbSIsInVwbiI6ImFkbWluMzY1QHNlc2llZHVyci5vbm1pY3Jvc29mdC5jb20iLCJ1dGkiOiJuSVNGbk5FTnZFbTlYNW1PbWJwNEFBIiwidmVyIjoiMS4wIiwid2lkcyI6WyI2MmU5MDM5NC02OWY1LTQyMzctOTE5MC0wMTIxNzcxNDVlMTAiXSwieG1zX3N0Ijp7InN1YiI6IjRGMEExR0cwX0RsVnUyMi1Id1owSlRNdGNTajFSUWF5NUpJQ3lCbjhqc1EifSwieG1zX3RjZHQiOjE1NDgyNzU4ODN9.h1dMyBbjpWHazNy2nVKhirfhxJ2DVfPYA-XIPzC5XTI2kvXJLdpW1dy7qTPWOgBgQ-SZpkJjlO_OtJmxUjOCm6vX1m4CIeL7pzNaOONxfYm4vvJ3DsWoCNLW_8M49_gFH_50NFhfaAnarsPl8G0JO-5KUmMniQAaoHsaiXfDUiN7HWdBZJgyn4GtG_Kl0vijUaRTnk_DKp0WrxZLg-B2aSA2LRzKjhtFeJ0o4YsL5-zIr3BqO6B0z48w6r1ZwhkyS4hqm-J6VQ0tyKi0Gjb-Iu-I6Ao_nrnK_Dy9AQX1JiIn31Q79e3ZPKXvpY-sh2RNvIzZY0uIliIJYW7383WHzg";
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                        //var response = client.GetAsync($"https://graph.microsoft.com/v1.0/groups?$filter=mailNickName eq 'Section_" + item + "'").Result;

                        //dynamic content = JsonConvert.DeserializeObject(
                        //    response.Content.ReadAsStringAsync()
                        //    .Result);

                        //if (response.IsSuccessStatusCode)
                        //{
                        //  var id = content.value[0].id;

                        //var json = "{'displayName': '" + item.nome + "'}";

                        //var contentString = new StringContent(json, Encoding.UTF8, "application/json");
                        //contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        //var responseUpdate = client.PatchAsync($"https://graph.microsoft.com/v1.0/groups/" + id.ToString(), contentString).Result;

                        //dynamic contentUpdate = JsonConvert.DeserializeObject(
                        //responseUpdate.Content.ReadAsStringAsync()
                        //.Result);

                        var jsonPut = "{'memberSettings': {'allowCreateUpdateChannels': true},'messagingSettings': {'allowUserEditMessages': true,'allowUserDeleteMessages': true},'funSettings': {'allowGiphy': true,'giphyContentRating': 'strict'}}";
                        var contentPutString = new StringContent(jsonPut, Encoding.UTF8, "application/json");
                        contentPutString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        var responseClassUpdate = client.PutAsync($"https://graph.microsoft.com/v1.0/groups/" + item + "/team", contentPutString).Result;

                        dynamic contentClassUpdate = JsonConvert.DeserializeObject(
                        responseClassUpdate.Content.ReadAsStringAsync()
                        .Result);

                        if (responseClassUpdate.IsSuccessStatusCode)
                        {
                            result.Add(item + ",sucesso,");
                        }
                        else
                        {
                            if (contentClassUpdate.error.message.ToString().IndexOf("exists") > -1)
                            {
                                result.Add(item + ",sucesso,Time já existe");
                            }
                            else
                            {
                                result.Add(item + ",erro," + contentClassUpdate.error.message.ToString());
                            }

                        }
                        //}
                        //else
                        //{
                        //    result.Add(item + ",erro,Grupo não encontrado, ou erro de accessToken.");
                        //}
                    }
                }
                catch (Exception ex)
                {

                }

            });

        }
    }
}
