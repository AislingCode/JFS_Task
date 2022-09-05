using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JFS_Task
{
    [Route("[controller]")]
    public class BalancesController : Controller
    {
        List<Balance>? Balances;

        // POST <BalancesController>/GetBalances
        [HttpPost("GetBalances")]
        public ActionResult GetBalances(IFormFile balance, IFormFile payments, FileFormat format)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.MissingMemberHandling = MissingMemberHandling.Error;

            using (Stream s = balance.OpenReadStream())
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JSONParserSM stateMachine = new JSONParserSM { ArrayName = "balance", State = State.LookingForArray };
                List<Balance> balances = new List<Balance>();

                while (reader.Read())
                {
                    switch (stateMachine.State)
                    {
                        case State.LookingForArray:
                            if (reader.Value != null && reader.Value.ToString() == stateMachine.ArrayName)
                            {
                                stateMachine.State = State.LookingForObject;
                            }
                            break;
                        case State.LookingForObject:
                            if (reader.TokenType == JsonToken.StartObject)
                            {
                                balances.Add(serializer.Deserialize<Balance>(reader));
                            }
                            else if (reader.TokenType == JsonToken.EndArray)
                            {
                                stateMachine.State = State.LookingForArray;
                            }
                            break;
                        default:
                            break;
                    }

                    // deserialize only when there's "{" character in the stream
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        //Balances = JsonConvert.DeserializeObject<List<Balance>>(reader);
                    }
                }
            }

            TempData["Message"] = "Controller executed; format selected: " + format.ToString();

            return Redirect("~/");
        }
    }
}
