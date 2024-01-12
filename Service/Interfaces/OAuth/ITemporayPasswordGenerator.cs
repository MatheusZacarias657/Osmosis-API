namespace Service.Interfaces.OAuth
{
    public interface ITemporayPasswordGenerator
    {
        bool Decode(string otp, string decodeKey);
        string Generate(string encodeKey);
    }
}