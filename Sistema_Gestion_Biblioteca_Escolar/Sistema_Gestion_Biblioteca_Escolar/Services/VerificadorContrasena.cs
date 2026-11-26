using System;
using System.Security.Cryptography;
using System.Web.Security;
using Sistema_Gestion_Biblioteca_Escolar.Data;
using Sistema_Gestion_Biblioteca_Escolar.Services;

namespace Sistema_Gestion_Biblioteca_Escolar.Services
{
    public static class VerificadorContrasena
    {
        // Código auxiliar sugerido por ChatGPT y adaptado al formato
        // de la cuenta de prueba: Base64 de 16 bytes de sal y 32 de hash.
        public static bool Verificar(string contrasena, string hashGuardado)
        {
            if (string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(hashGuardado))
            {
                return false;
            }

            byte[] datos;
            try
            {
                datos = Convert.FromBase64String(hashGuardado);
            }
            catch (FormatException)
            {
                return false;
            }

            const int longitudSal = 16;
            const int longitudHash = 32;

            if (datos.Length != longitudSal + longitudHash)
            {
                return false;
            }

            var sal = new byte[longitudSal];
            Array.Copy(datos, sal, longitudSal);

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                contrasena, sal, 600000, HashAlgorithmName.SHA256))
            {
                var hashCalculado = pbkdf2.GetBytes(longitudHash);
                int diferencia = 0;

                for (int i = 0; i < longitudHash; i++)
                {
                    diferencia |= hashCalculado[i] ^ datos[longitudSal + i];
                }

                return diferencia == 0;
            }
        }
    }
}