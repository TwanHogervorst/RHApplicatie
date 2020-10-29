using Newtonsoft.Json;

namespace RHApplicatieLib.Abstract
{
    public abstract class DAbstract
    {

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
