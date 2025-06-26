
using Microsoft.AspNetCore.Mvc;

using System.Net.Http.Headers;

using System.Text.Json;

using System.Text;
using static System.Net.WebRequestMethods;
using static System.Collections.Specialized.BitVector32;


namespace project.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {

        private static readonly HttpClient client;

        static TrainController()
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            Console.OutputEncoding = System.Text.Encoding.UTF8;


        }
        [HttpGet("survey")]
        public async Task<List<Survey>> SurveryInfo()
        {
            await using Stream stream =
                await client.GetStreamAsync("https://api.odpt.org/api/v4/odpt:PassengerSurvey?acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");


            var surveys = await JsonSerializer.DeserializeAsync<List<Survey>>(stream);



            return surveys;

        }


        [HttpGet("raildirection")]
        public async Task<List<RailwayDirection>> RailDirectionInfo()
        {
            await using Stream stream =
                await client.GetStreamAsync("https://api.odpt.org/api/v4/odpt:RailDirection?acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");
            var RailwayDirections = await JsonSerializer.DeserializeAsync<List<RailwayDirection>>(stream);



            return RailwayDirections;

        }



        [HttpGet("railway")]
        public async Task<List<Railway>> RailwayInfo()
        {
            Stream stream =
                await client.GetStreamAsync("https://api.odpt.org/api/v4/odpt:Railway?acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");

            
            
            
            var Railway = await JsonSerializer.DeserializeAsync<List<Railway>>(stream);



            return Railway;

        }


        [HttpGet("railwayfare")]
        public async Task<List<RailwayFare>> RailwayFareInfo()
        {


            await using Stream stream =
                await client.GetStreamAsync("https://api.odpt.org/api/v4/odpt:RailwayFare?acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");
            var RailwayFare = await JsonSerializer.DeserializeAsync<List<RailwayFare>>(stream);



            return RailwayFare;

        }

        [HttpGet("station")]
        public async Task<List<Station>> StationInfo(string? StationName = null)
        {
            Stream stream = await client.GetStreamAsync("https://api.odpt.org/api/v4/odpt:Station?acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw"); 
            if (!string.IsNullOrEmpty(StationName))
            {
                StationName = StationName.ToLower();
                StationName = char.ToUpper(StationName[0]) + StationName.Substring(1);

                stream = await client.GetStreamAsync($"https://api.odpt.org/api/v4/odpt:Station?odpt:stationTitle.en={StationName}&acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");
            }

             var Station = await JsonSerializer.DeserializeAsync<List<Station>>(stream);



            return Station;

        }


        [HttpGet("stationtimetable")]
        public async Task<List<StationTimetable>> StationTimetableInfo()
        {
            await using Stream stream =
                await client.GetStreamAsync("https://api.odpt.org/api/v4/odpt:StationTimetable?acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");
            var StationTimetable = await JsonSerializer.DeserializeAsync<List<StationTimetable>>(stream);



            return StationTimetable;

        }




        [HttpGet("traininformation")]
        public async Task<List<TrainInformation>> TrainInfo()
        {
            await using Stream stream =
                await client.GetStreamAsync("https://api.odpt.org/api/v4/odpt:TrainInformation?acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");
            var TrainInfo = await JsonSerializer.DeserializeAsync<List<TrainInformation>>(stream);


            var jatexts = new List<object>();

            foreach (var info in TrainInfo) {
                jatexts.Add(new { text = info.TrainInformationText.Ja });
                    
            }

            var arrjatexts = jatexts.ToArray(); 


            string endpoint = "https://api.cognitive.microsofttranslator.com";
            string route = "/translate?api-version=3.0&from=ja&to=en";

            
            var requestBody = JsonSerializer.Serialize(arrjatexts);


            using (var request = new HttpRequestMessage()) {

                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", "19Ji0rS0oTCnHGGt7xPFudFMLBMKxwzLlR6MnbHPHcPj4xkHT9PQJQQJ99ALAClhwhEXJ3w3AAAbACOGXKkp");
                request.Headers.Add("Ocp-Apim-Subscription-Region", "ukwest");

                var response = await client.SendAsync(request);

                var result = await response.Content.ReadAsStreamAsync();

              
                var traresult = await JsonSerializer.DeserializeAsync<List<translation>>(result);

                List<TrainInformation> updated = new List<TrainInformation>();

                for (int i = 0; i < TrainInfo.Count; i++) { 
                    updated.Add(new TrainInformation(
                        Context: TrainInfo[i].Context,
                        Id: TrainInfo[i].Id,
                        Type: TrainInfo[i].Type,
                        Date: TrainInfo[i].Date,
                        SameAs: TrainInfo[i].SameAs,
                        Valid: TrainInfo[i].Valid,
                        TimeOfOrigin: TrainInfo[i].TimeOfOrigin,
                        Operator: TrainInfo[i].Operator,
                        Railway: TrainInfo[i].Railway,
                        TrainInformationText: new Titles(
                            Ja: TrainInfo[i].TrainInformationText.Ja,
                            En: traresult[i].translations[0].text
                        )
                    ));

                }
                


                return updated;


            }

            

        }


        [HttpGet("traintimetable")]
        public async Task<List<TrainTimetable>> TrainTimetableInfo()
        {
            await using Stream stream =
                await client.GetStreamAsync("https://api.odpt.org/api/v4/odpt:TrainTimetable?acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");
            var TrainTimetableInfo = await JsonSerializer.DeserializeAsync<List<TrainTimetable>>(stream);



            return TrainTimetableInfo;

        }


        [HttpGet("traintype")]
        public async Task<List<TrainType>> TrainTypeinfo()
        {
            await using Stream stream =
             await client.GetStreamAsync("https://api.odpt.org/api/v4/odpt:TrainType?acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");
            var TrainTypeInfo = await JsonSerializer.DeserializeAsync<List<TrainType>>(stream);



            return TrainTypeInfo;

        }

        [HttpGet("stationandtimetable")]
        public async Task<List<List<StationTimetable>>> StationAndTimetable(string station) {
            station = station.ToLower();
            station = char.ToUpper(station[0]) + station.Substring(1);

            await using Stream stream =
                await client.GetStreamAsync($"https://api.odpt.org/api/v4/odpt:Station?odpt:stationTitle.en={station}&acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");

            var specifiedStation = await JsonSerializer.DeserializeAsync<List<Station>>(stream);
            List<List<StationTimetable>> timetablesAtStations = new List<List<StationTimetable>>();
            for (int i = 0; i < specifiedStation.Count; i++) {
                await using Stream timetablestream =
                    await client.GetStreamAsync($"https://api.odpt.org/api/v4/odpt:StationTimetable?odpt:station={specifiedStation[i].SameAs}&acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");
                var specifictimetable = await JsonSerializer.DeserializeAsync<List<StationTimetable>>(timetablestream);
                if (specifictimetable.Count > 0) {
                    timetablesAtStations.Add(specifictimetable);

                }
                

            }


            return timetablesAtStations;





        }

        [HttpGet("stationandsurvey")]
        public async Task<List<List<Survey>>> StationAndSurvey(string station)
        {
            station = station.ToLower();
            station = char.ToUpper(station[0]) + station.Substring(1);

            await using Stream stream =
                await client.GetStreamAsync($"https://api.odpt.org/api/v4/odpt:Station?odpt:stationTitle.en={station}&acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");

            var specifiedStation = await JsonSerializer.DeserializeAsync<List<Station>>(stream);
            List<List<Survey>> surveysAtStations = new List<List<Survey>>();
            for (int i = 0; i < specifiedStation.Count; i++)
            {
                await using Stream surveystream =
                    await client.GetStreamAsync($"https://api.odpt.org/api/v4/odpt:PassengerSurvey?odpt:station={specifiedStation[i].SameAs}&acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");
                var specificsurvey = await JsonSerializer.DeserializeAsync<List<Survey>>(surveystream);
                if (specificsurvey.Count > 0)
                {
                    surveysAtStations.Add(specificsurvey);

                }


            }


            return surveysAtStations;


        }

        [HttpGet("stationandrailwayfare")]
        public async Task<List<RailwayFare>> StationAndFare(string ToStation, string FromStation) {
            ToStation = ToStation.ToLower();
            ToStation = char.ToUpper(ToStation[0]) + ToStation.Substring(1);

            FromStation = FromStation.ToLower();
            FromStation = char.ToUpper(FromStation[0]) + FromStation.Substring(1);


            await using Stream Tostream =
                await client.GetStreamAsync($"https://api.odpt.org/api/v4/odpt:Station?odpt:stationTitle.en={ToStation}&acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");

            var specifiedToStation = await JsonSerializer.DeserializeAsync<List<Station>>(Tostream);


            await using Stream Fromstream =
                await client.GetStreamAsync($"https://api.odpt.org/api/v4/odpt:Station?odpt:stationTitle.en={FromStation}&acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");

            var specifiedFromStation = await JsonSerializer.DeserializeAsync<List<Station>>(Fromstream);


           

            await using Stream FareStream =
                await client.GetStreamAsync($"https://api.odpt.org/api/v4/odpt:RailwayFare?odpt:fromStation={specifiedFromStation[0].SameAs}&odpt:toStation={specifiedToStation[0].SameAs}&acl:consumerKey=n3un6lz0zt4wmw718pbquqmxiw215ssbxjvb0x5bqr5mgwnofsiwogvkstiu4vyw");
            var specificjourney = await JsonSerializer.DeserializeAsync<List<RailwayFare>>(FareStream);

            return specificjourney;

        }






    }
}
