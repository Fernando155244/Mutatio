using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mutation
{
    /*En esta pantalla podemos ver los datos y confirmar si estamos en la pantalla correcta*/
    [Activity(Label = "AcConfirmación")]
    public class AcConfirmación : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Confirmacion);

            //Indexados
            TextView solicitud = this.FindViewById<TextView>(Resource.Id.txtConfirmacionTipoSolicitudDato);
            TextView Nombre = this.FindViewById<TextView>(Resource.Id.txtConfirmacionNombreDato);
            TextView Actual = this.FindViewById<TextView>(Resource.Id.txtConfiracionEstadoDato);
            TextView Opcion1 = this.FindViewById<TextView>(Resource.Id.txtConfirmacionOpcion1Dato);
            TextView Opcion2 = this.FindViewById<TextView>(Resource.Id.txtConfirmacionOpcion2Dato);
            TextView Nivel = this.FindViewById<TextView>(Resource.Id.txtConfirmacionNivelDatos);
            Button btnConfirmacion = this.FindViewById<Button>(Resource.Id.btnConfirmacion);

            //Definición de datos
            solicitud.Text = "Cambios";
            Nombre.Text = "Fernando";
            Actual.Text = "Universidad";
            Opcion1.Text = "Tlatelolco";
            Opcion2.Text = "HomeOffice";
            Nivel.Text = "Practicante";

            //Funcion de boton
            btnConfirmacion.Click += BtnConfirmacion_Click;

        }
        /*Si los datos son correctos este boton nos permite ir a la proxima pantalla*/
        private void BtnConfirmacion_Click(object sender, EventArgs e)
        {
            //Esto es solo para mostrar que se envia, se tiene que borrar
            Toast.MakeText(this, $"Folio {this.Intent.GetStringExtra("Folio")}", ToastLength.Long).Show();


            Intent Confirmado = new Intent(this, typeof(AcInicio));
            Confirmado.PutExtra("Folio", this.Intent.GetStringExtra("Folio"));
            StartActivity(Confirmado);
        }
    }
}