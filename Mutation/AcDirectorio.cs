using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Javax.Security.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace Mutation
{
    [Activity(Label = "AcDirectorio")]
    public class AcDirectorio : Activity
    {
        private String[] ds = { "Fernando Omar Orozco Ruiz", "Benito Juárez García", "Miguel Hidalgo y Costilla", "Antonio López de Santa Anna Pérez de Lebrón" };
        private String[] nds = { "Practicante de TI", "Maestro de la UNAM", "Director de la Real y Pontifica Universidad de la Nueva España", "Jefe de seguridad" };
        private String[] cds = { "155244@udlodnres.com", "benemeritodelasamerica@gmail.com", "padredelaindependencia@hotmail.com", "mipierna!@outlook.com" };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Directorio);
            //Index y definición de logo
            ImageView imgLogo = this.FindViewById<ImageView>(Resource.Id.Logo);
            imgLogo.SetImageResource(Resource.Drawable.Logo);

            //Indexados
            ListView lsDirectorio = this.FindViewById<ListView>(Resource.Id.lsDirecotio);

            //Mandamos a llenar la lista
            lsDirectorio.Adapter = new RellenarDirectorio (this, ds, nds, cds);
        }
    }

    internal class RellenarDirectorio : BaseAdapter
    {
        private AcDirectorio acDirectorio;
        private string[] ds;
        private string[] nds;
        private string[] cds;

        public RellenarDirectorio(AcDirectorio acDirectorio, string[] ds, string[] nds, string[] cds)
        {
            this.acDirectorio = acDirectorio;
            this.ds = ds;
            this.nds = nds;
            this.cds = cds;
        }

        public override int Count
        {
            get
            {
                return ds.Length;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return "";
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View vista = convertView;
            //Declaración de vista
            if (vista == null)
            {
                //Aqui aparte de declarar los valores basicos declaramos el uso del diseño TabEmpresa para la fila
                vista = acDirectorio.LayoutInflater.Inflate(Resource.Layout.ModDirectorio, null);
            }
            TextView Estado = vista.FindViewById<TextView>(Resource.Id.txtModDirectorioNombre);
            TextView Numeros = vista.FindViewById<TextView>(Resource.Id.txtModDirectorioPuesto);
            TextView Correo = vista.FindViewById<TextView>(Resource.Id.txtModDirectorioCorreo);
            Estado.Text = ds[position];
            Numeros.Text = nds[position];
            Correo.Text = cds[position];
            return vista;
        }
    }
}