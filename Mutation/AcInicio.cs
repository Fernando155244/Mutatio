using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Mutation
{
    [Activity(Label = "AcInicio")]
    public class AcInicio : Activity
    {
        clsDatos datos = new clsDatos();
        DataSet ds = new DataSet();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Inicio);

            //Mandamos a pedir los datos para analizar los datos
            Llenar();

            //Index y definición de logo
            ImageView imgLogo = this.FindViewById<ImageView>(Resource.Id.Logo);
            imgLogo.SetImageResource(Resource.Drawable.Logo);

            bool Recibido = true;
            Date dia = new Date(2000,04,15);
            String Opcion = "Tu casa y promovido a Cliente";
            String Nombre = "Fernando Omar Orozco Ruiz";
            int resultado = 1;

            //Indexados
            TextView datSolicitud = this.FindViewById<TextView>(Resource.Id.txtInicioDatosSolicitud);
            TextView datosSolicitante = this.FindViewById<TextView>(Resource.Id.txtInicioNombreSolicitante);
            TextView recibir = this.FindViewById<TextView>(Resource.Id.txtInicioRegistrado);
            TextView confirmacion = this.FindViewById<TextView>(Resource.Id.txtInicioConfirmacion);
            TextView Estatus = this.FindViewById<TextView>(Resource.Id.txtInicioEstatusDato);
            TextView Observaciones = this.FindViewById<TextView>(Resource.Id.txtObservacionesDato);
            LinearLayout Notificacion = this.FindViewById<LinearLayout>(Resource.Id.lyInicioNotificacion);
            ImageView ImgNotificacion = this.FindViewById<ImageView>(Resource.Id.lblInicioNotificacionImagen);
            TextView TituloNotificacion = this.FindViewById<TextView>(Resource.Id.txtInicioNotificacionTitulo);
            TextView LeyendaNotificacion = this.FindViewById<TextView>(Resource.Id.txtInicioNotificacionLeyenda);
            TextView Asking = this.FindViewById<TextView>(Resource.Id.lblPregutnas);

            Asking.Click += Asking_Click;

            //Con esto ingresamos los dtos de la consulta
            datSolicitud.Text = $"Solicitud de Cambios Numero: {this.Intent.GetStringExtra("Folio")}";
            //Con esto ingresamos el nombre del usuario
            datosSolicitante.Text = $"Del trabajador: {Nombre}";
            //Iniciamos las restricciónes para definir estados
            //Recibimos el verdadero o falso del usuario
            if (Recibido)
            {
                //Si se ha recibido la solicitud se hara visible recibir y nos data una leyenda con día de llegada de solicitud
                recibir.Visibility = ViewStates.Visible;
                recibir.Text = $"🔘Solicitud Registrada {dia.Day}/{dia.Month}/{dia.Year}";
                //Si el resultado de la solicitud es 0
                if(resultado== 0)
                {
                    //hacemos invisible la confirmación y la notificación
                    confirmacion.Visibility = ViewStates.Invisible;
                    Notificacion.Visibility = ViewStates.Invisible;
                }
                else
                {
                    //Si el resultado es 1 damos los resultados de que fue aceptada la solicitud
                    if (resultado == 1)
                    {
                        //Hacemos visible la confirmación
                        confirmacion.Visibility = ViewStates.Visible;
                        //Damos la leyenda de confirmación
                        confirmacion.Text = $"🔘La solicitud fue Aceptada {dia.Day}/{dia.Month}/{dia.Year}";
                        //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no)
                        Notificacion.Visibility = ViewStates.Visible;
                        //Damos imagen de positivo a la notificación
                        ImgNotificacion.SetImageResource(Resource.Drawable.Positivo);
                        //Damos titulo de ser correcta
                        TituloNotificacion.Text = "Felicidades!";
                        //Damos una leyenda feliciatando al usuario
                        LeyendaNotificacion.Text = $"Has sido aceptado para transferencia a {Opcion}";
                    }
                    //Si el resultado fue 2 damos resultado de fue negada la solicitud
                    else if (resultado == 2)
                    {
                        //Hacemos visible la confirmación
                        confirmacion.Visibility = ViewStates.Visible;
                        //Damos la leyenda de confirmación
                        confirmacion.Text = $"🔘La solicitud fue Denegada {dia.Day}/{dia.Month}/{dia.Year}";
                        //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no)
                        Notificacion.Visibility = ViewStates.Visible;
                        //Damos la imagen de denegación de solicitud
                        ImgNotificacion.SetImageResource(Resource.Drawable.Negativo);
                        //Damos titulo de negación
                        TituloNotificacion.Text = "Lastima";
                        //Damos leyenda de notificación de negación
                        LeyendaNotificacion.Text = $"No has sido aceptado para transferencia";
                    }
                    //Si el resultado fue 3 damos aviso de que la solicitud fue cancelada
                    else if (resultado == 3)
                    {
                        //Hacemos visible la confirmación
                        confirmacion.Visibility = ViewStates.Visible;
                        //Ingresamos el aviso de que la solicitud fue cancelada
                        confirmacion.Text = $"🔘La solicitud fue Cancelada {dia.Day}/{dia.Month}/{dia.Year}";
                        //Mantenemos la solicitud invisible
                        Notificacion.Visibility = ViewStates.Invisible;
                    }
                    else
                    {
                        //En caso de no ser ninguno de los anteriores mandamos aviso de error para que se comunique con servicio tecnico
                        confirmacion.Visibility = ViewStates.Visible;
                        //Ingresamos la leyenda
                        confirmacion.Text = $"🔘Error, por favor comuniquese a servicio tecnico";
                        //Mantenemo invisible la notificación
                        Notificacion.Visibility = ViewStates.Invisible;
                    }
                }
            }
            else
            {
                //Mantenemos todo invisible
                datSolicitud.Visibility = ViewStates.Invisible;
            }
            //Textos inferiores de datos extra de la solicitud
            Estatus.Text = "En revision";
            Observaciones.Text = "Le falto ordenar su papeleo Wasowsky!";
        }

        private async void Llenar()
        {
            ds = await datos.Iniciar(this.Intent.GetIntExtra("Folio", 0), this.Intent.GetIntExtra("Tipo", 0));
        }

        private void Asking_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AcPreguntas));
        }
    }
}