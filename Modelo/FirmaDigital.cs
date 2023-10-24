using System.Security.Cryptography;
using System.Text;
using System.Numerics;

namespace LAB01_EDII.Modelo
{
    public class FirmaDigital
    {
        private RSACryptoServiceProvider rsa;
        //por defecto genera un par de claves publicas y privadas el RSACrypto
        // Constructor que genera las claves
        public FirmaDigital()
        {
            //implementacion de NET de RSA
            rsa = new RSACryptoServiceProvider();//instancia con los metodos signhash y verifyhash para firmar y verificar firma, internamente maneja las claves publicas y privadas
        }
        //obtener claves 
        public PublicKey GetPublicKey()
        {
            RSAParameters publicKey = rsa.ExportParameters(false);
            return new PublicKey(publicKey.Modulus, publicKey.Exponent);
        }

        public PrivateKey GetPrivateKey()
        {
            RSAParameters privateKey = rsa.ExportParameters(true);
            return new PrivateKey(privateKey.Modulus, privateKey.D);
        }

        //tomo la conversacion que quiero firmar y calculo el hash luego firmo ese hash con la clave privada rsa.sighhash()
        public byte[] GenerarFirma(string message)
        {
            // Calcula el resumen (hash) del mensaje
            SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(message));

            // Firma el hash con la clave privada
            return rsa.SignHash(hash, CryptoConfig.MapNameToOID("SHA256"));
        }
        //uso rsa.verifyhash, para verificar si la firma es valida para ese hash, usando la clave publica internamente
        public bool ValidarFirma(string message, byte[] signature)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(message));

            // Verifica la firma con la clave publica
            return rsa.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA256"), signature);
        }
    }

    public class PrivateKey
    {
        public BigInteger N { get; }
        public BigInteger D { get; }

        public PrivateKey(byte[] n, byte[] d)
        {
            N = new BigInteger(n);
            D = new BigInteger(d);
        }
    }

    public class PublicKey
    {
        public BigInteger N { get; }
        public BigInteger K { get; }

        public PublicKey(byte[] n, byte[] k)
        {
            N = new BigInteger(n);
            K = new BigInteger(k);
        }
    }
}

