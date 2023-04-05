namespace Services.Integration.Core
{
    public interface IMessageSecurityProvider
    {
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
    }
}
