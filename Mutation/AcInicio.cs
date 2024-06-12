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
using System.Drawing;
using System.Linq;
using System.Text;

namespace Mutation
{
    [Activity(Label = "AcInicio")]
    public class AcInicio : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Inicio);
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

            datSolicitud.Text = $"Solicitud de Cambios Numero: {this.Intent.GetStringExtra("Folio")}";
            datosSolicitante.Text = $"Del trabajador: {Nombre}";
            if (Recibido)
            {
                recibir.Visibility = ViewStates.Visible;
                recibir.Text = $"Solicitud Registrada {dia.Day}/{dia.Month}/{dia.Year}";
                if(resultado== 0)
                {
                    confirmacion.Visibility = ViewStates.Invisible;
                    Notificacion.Visibility = ViewStates.Invisible;
                }
                else
                {
                    if (resultado == 1)
                    {
                        confirmacion.Visibility = ViewStates.Visible;
                        confirmacion.Text = $"La solicitud fue Aceptada {dia.Day}/{dia.Month}/{dia.Year}";
                        Notificacion.Visibility = ViewStates.Visible;
                        ImgNotificacion.SetImageResource(Resource.Drawable.Positivo);
                        TituloNotificacion.Text = "Felicidades!";
                        LeyendaNotificacion.Text = $"Has sido aceptado para transferencia a {Opcion}";
                    }
                    else if (resultado == 2)
                    {
                        confirmacion.Visibility = ViewStates.Visible;
                        confirmacion.Text = $"La solicitud fue Denegada {dia.Day}/{dia.Month}/{dia.Year}";
                        Notificacion.Visibility = ViewStates.Visible;
                        ImgNotificacion.SetImageResource(Resource.Drawable.Negativo);
                        TituloNotificacion.Text = "Lastima";
                        LeyendaNotificacion.Text = $"No has sido aceptado para transferencia";
                    }
                    else if (resultado == 3)
                    {
                        confirmacion.Visibility = ViewStates.Visible;
                        confirmacion.Text = $"La solicitud fue Cancelada {dia.Day}/{dia.Month}/{dia.Year}";
                        Notificacion.Visibility = ViewStates.Invisible;
                    }
                    else
                    {
                        confirmacion.Visibility = ViewStates.Visible;
                        confirmacion.Text = $"Error, por favor comuniquese a servicio tecnico";
                        Notificacion.Visibility = ViewStates.Invisible;
                    }
                }
            }
            else
            {
                datSolicitud.Visibility = ViewStates.Invisible;
            }
            Estatus.Text = "En revision";
            Observaciones.Text = "Le falto ordenar su papeleo Wasowsky!";
        }
    }
}