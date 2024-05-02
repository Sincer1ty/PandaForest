using Newtonsoft.Json.Linq;

public interface IJsonSaveable
{
    JToken CaptureAsJToken();
    void RestroreFromJToken(JToken state);
}