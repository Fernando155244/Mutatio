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

namespace Mutation
{
    /*En esta pantalla podemos ver los datos y confirmar si estamos en la pantalla correcta*/
    [Activity(Label = "AcConfirmación")]
    public class AcConfirmación : Activity
    {
        clsDatos datos = new clsDatos();
        DataSet ds = new DataSet();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Confirmacion);

            //Index y definición de logo
            ImageView imgLogo = this.FindViewById<ImageView>(Resource.Id.Logo);
            imgLogo.SetImageResource(Resource.Drawable.Logo);

            Button btnConfirmacion = this.FindViewById<Button>(Resource.Id.btnConfirmacion);
            llenarBD();
           

            //Funcion de boton
            btnConfirmacion.Click += BtnConfirmacion_Click;

        }
        private async void llenarBD()
        {
            //Indexados
            TextView solicitud = this.FindViewById<TextView>(Resource.Id.txtConfirmacionTipoSolicitudDato);
            TextView Nombre = this.FindViewById<TextView>(Resource.Id.txtConfirmacionNombreDato);
            TextView Actual = this.FindViewById<TextView>(Resource.Id.txtConfiracionEstadoDato);
            TextView Opcion1 = this.FindViewById<TextView>(Resource.Id.txtConfirmacionOpcion1Dato);
            TextView Opcion2 = this.FindViewById<TextView>(Resource.Id.txtConfirmacionOpcion2Dato);
            TextView Nivel = this.FindViewById<TextView>(Resource.Id.txtConfirmacionNivelDatos);
            try
            {
                //Mandamos a llenar la función
                ds = await datos.Revisar(this.Intent.GetIntExtra("Folio", 0), this.Intent.GetIntExtra("Tipo", 0));
                //Definición de datos
                if (this.Intent.GetIntExtra("Tipo", 0) == 0)
                {
                    solicitud.Text = "Cambios";
                    Nombre.Text = $"{ds.Tables[0].Rows[0]["paterno"].ToString()} {ds.Tables[0].Rows[0]["materno"].ToString()} {ds.Tables[0].Rows[0]["nombres"].ToString()}";
                    Actual.Text = $"{ds.Tables[0].Rows[0]["Actual"].ToString()}";
                    Opcion1.Text = $"{ds.Tables[0].Rows[0]["Opcion1"].ToString()}";
                    Opcion2.Text = $"{ds.Tables[0].Rows[0]["Opcion2"].ToString()}";
                    Nivel.Text = $"{ds.Tables[0].Rows[0]["nivel"].ToString()}";
                }
                else
                {
                    solicitud.Text = "Permutas";
                    Nombre.Text = $"{ds.Tables[0].Rows[0]["paterno"].ToString()} {ds.Tables[0].Rows[0]["materno"].ToString()} {ds.Tables[0].Rows[0]["nombres"].ToString()}";
                    Actual.Text = $"{ds.Tables[0].Rows[0]["Actual"].ToString()}";
                    Opcion1.Text = $"{ds.Tables[0].Rows[0]["Opcion"].ToString()}";
                    Opcion2.Text = $"Null";
                    Nivel.Text = $"{ds.Tables[0].Rows[0]["nivel"].ToString()}";
                }
            }
            catch (Exception ex)
            {
                //Toast.MakeText(this,$"Error en los datos, intente de nuevo {ex}", ToastLength.Long).Show();
                AlertDialog a1 = new AlertDialog.Builder(this).Create();
                a1.SetTitle("Alerta!");
                a1.SetMessage($"Error en los datos, intente de nuevo {ex}");
                a1.SetButton("OK", btnOK);
                a1.Show();
            }
            

        }

        private void btnOK(object sender, DialogClickEventArgs e)
        {
            Finish();
        }

        /*Si los datos son correctos este boton nos permite ir a la proxima pantalla*/
        private void BtnConfirmacion_Click(object sender, EventArgs e)
        {
            Intent Confirmado = new Intent(this, typeof(AcInicio));
            Confirmado.PutExtra("Folio",this.Intent.GetIntExtra("Folio",0));
            Confirmado.PutExtra("Tipo", this.Intent.GetIntExtra("Tipo", 0));
            StartActivity(Confirmado);
        }
    }
}