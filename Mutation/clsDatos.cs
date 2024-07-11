using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutation
{
    [Activity(Label = "clsDatos")]
    public class clsDatos : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
        }
        //Función que llama al servidor para verificar que se conecte a la base de datos 
        private bool HelloWorld()
        {
            //Hacemos una conexion con el servicio en linea
            Conexion.Mutation ws = new Conexion.Mutation();
            //Generamos la variable para recibir una respuesta
            bool ds = false;
            //mandamos a pedir los datos al servidor con el cliente
            ds = ws.HelloWorld();
            //regresamos el valor
            return ds;
        }
        //Función asimetrica
        public Task<bool> Conectar()
        {
            //Llama a la fución HelloWorld para pedir el bool
            return Task.Run(()=>HelloWorld());
        }
        //Función para el login de la solicitud
        private bool Login(int NoSolicitud, String RFC, int Tipo)
        {
            //Hacemos una conexion con el servicio en linea
            Conexion.Mutation ws = new Conexion.Mutation();
            //Generamos la variable para recibir una respuesta
            bool ds;
            //mandamos a pedir los datos al servidor con el cliente
            ds = ws.Login(NoSolicitud, RFC, Tipo);
            //regresamos el valor
            return ds;
        }
        public Task<bool> Sesion(int NoSolicitud, String RFC, int Tipo)
        {
            //Llamamos a la función Login para pedir el valor de manera asimetrica
            return Task.Run(()=>Login(NoSolicitud,RFC, Tipo));
        }
        private DataSet Revision(int NoSolicitud, int Tipo)
        {
            //Hacemos una conexion con el servicio en linea
            Conexion.Mutation ws = new Conexion.Mutation();
            //Generamos la variable para recibir una respuesta
            DataSet ds;
            //mandamos a pedir los datos al servidor con el cliente
            ds = ws.Revision(NoSolicitud, Tipo);
            //regresamos el valor
            return ds;
        }
        public Task<DataSet> Revisar(int NoSolicitud, int Tipo)
        {
            //Llamamos a la función Login para pedir Los valores de manera asimetrica
            return Task.Run(()=>Revision(NoSolicitud, Tipo));
        }
        public DataSet Iniciar(int NoSolicitud, int Tipo)
        {
            //Hacemos una conexion con el servicio en linea
            Conexion.Mutation ws = new Conexion.Mutation();
            //Generamos la variable para recibir una respuesta
            DataSet ds;
            //mandamos a pedir los datos al servidor con el cliente
            ds = ws.Inicio(NoSolicitud,Tipo);
            //regresamos el valor
            return ds;
        }
        public DataSet graficadora(int referencias, int tipo, int certificadas, int canceladas, int status)
        {
            //Hacemos una conexion con el servicio en linea
            Conexion.Mutation ws = new Conexion.Mutation();
            //Generamos la variable para recibir una respuesta
            DataSet ds;
            //mandamos a pedir los datos al servidor con el cliente
            ds = ws.Graficas(referencias, tipo, certificadas, canceladas,status);
            //regresamos el valor
            return ds;
        }
    }
}