using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppMinas.Utilidades
{
    public static class UtilidadesAzure
    {
        public static string GuardarArchivo(string NombreCarpeta, string NombreArchivo, HttpPostedFile Archivo)
        {
            string NombreArchivoReal = Archivo.FileName;
            string[] ArrayExtension = NombreArchivoReal.Split('.');
            string Extension = ArrayExtension[ArrayExtension.Length - 1];
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(NombreCarpeta);
            container.CreateIfNotExists();
            container.SetPermissions(
            new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(NombreArchivo + "." + Extension);
            blockBlob.DeleteIfExists();
            blockBlob.UploadFromStream(Archivo.InputStream);
            return blockBlob.SnapshotQualifiedUri.AbsoluteUri;

        }

        public static string getNombreArchivoWeb(string RutaArchivo)
        {
            string[] vecRutaArchivo = RutaArchivo.Split('/');
            string NombreArchivoSinExtencion = "";
            if (vecRutaArchivo.Length > 0)
            {
                string NombreArvivo = vecRutaArchivo[vecRutaArchivo.Length - 1];
                string[] vecNombreArchivoSinExtencion = NombreArvivo.Split('.');
                if (vecNombreArchivoSinExtencion.Length > 0)
                {
                    NombreArchivoSinExtencion = vecNombreArchivoSinExtencion[vecNombreArchivoSinExtencion.Length - 1];
                }

            }
            return NombreArchivoSinExtencion;
        }

    }
}