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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mutation
{
    [Activity(Label = "AcInicio")]
    public class AcInicio : Activity
    {
        public DataSet ds = new DataSet();
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Inicio);

            //Mandamos a pedir los datos para analizar los datos
            int resultado = Llenar();

            //Index y definición de logo
            ImageView imgLogo = this.FindViewById<ImageView>(Resource.Id.Logo);
            imgLogo.SetImageResource(Resource.Drawable.Logo);
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
            try
            {
                Asking.Click += Asking_Click;
                //Con esto ingresamos los dtos de la consulta
                datSolicitud.Text = $"Solicitud de Cambios Numero: {this.Intent.GetIntExtra("Folio", 0)}";
                //Con esto ingresamos el nombre del usuario
                datosSolicitante.Text = $"Del trabajador: {ds.Tables[0].Rows[0]["paterno"].ToString()} {ds.Tables[0].Rows[0]["materno"].ToString()} {ds.Tables[0].Rows[0]["nombres"].ToString()}";
                Notificacion.Visibility = ViewStates.Invisible;
                if (this.Intent.GetIntExtra("Tipo", 0) == 0)
                {
                    //Iniciamos las restricciónes para definir estados
                    //Recibimos el verdadero o falso del usuario
                    if (resultado != 1)
                    {
                        //Si se ha recibido la solicitud se hara visible recibir y nos data una leyenda con día de llegada de solicitud
                        recibir.Visibility = ViewStates.Visible;
                        recibir.Text = $"🔘Solicitud Registrada {ds.Tables[0].Rows[0]["f_registro"].ToString()}";
                        //Si el resultado es 1 damos los resultados de que fue aceptada la solicitud
                        if (resultado == 2)
                        {
                            //Hacemos visible la cancelación de la solicitud
                            confirmacion.Visibility = ViewStates.Visible;
                            //Damos la leyenda de cancelación
                            confirmacion.Text = $"🔘La solicitud fue Cancelada";
                            //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no o cancelamos)
                            Notificacion.Visibility = ViewStates.Visible;
                            //Damos imagen de cancelado a la notificación
                            ImgNotificacion.SetImageResource(Resource.Drawable.Neutro);
                            //Damos titulo de concejo por cancelar
                            TituloNotificacion.Text = "Si así lo descea puede hacer otra solicitud";
                            //Damos una leyenda con las obsercaciones al cancelar al usuario
                            Observaciones.Text = $"{ds.Tables[0].Rows[0]["observaciones_cancelacion"].ToString()}";
                        }
                        else if (resultado == 5)
                        {
                            //Hacemos visible la Negativa
                            confirmacion.Visibility = ViewStates.Visible;
                            //Damos la leyenda de necación de solicitud
                            confirmacion.Text = $"🔘La solicitud fue Denegada";
                            //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no o cancelamos)
                            Notificacion.Visibility = ViewStates.Visible;
                            //Damos la imagen de denegación de solicitud
                            ImgNotificacion.SetImageResource(Resource.Drawable.Negativo);
                            //Damos titulo de negación
                            TituloNotificacion.Text = "Lastima";
                            //Damos leyenda de notificación de negación
                            LeyendaNotificacion.Text = $"No has sido aceptado para transferencia";
                        }
                        else if (resultado == 6)
                        {
                            //Hacemos visible la confirmación
                            confirmacion.Visibility = ViewStates.Visible;
                            //Damos la leyenda de confirmación
                            confirmacion.Text = $"🔘La solicitud fue Aceptada";
                            //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no)
                            Notificacion.Visibility = ViewStates.Visible;
                            //Damos imagen de positivo a la notificación
                            ImgNotificacion.SetImageResource(Resource.Drawable.Positivo);
                            //Damos titulo de ser correcta
                            TituloNotificacion.Text = "Felicidades!";
                            //Damos una leyenda feliciatando al usuario
                            LeyendaNotificacion.Text = $"Has sido aceptado para transferencia";
                        }
                        else if (resultado == 0)
                        {
                            //Hacemos visible la confirmación
                            confirmacion.Visibility = ViewStates.Visible;
                            //Damos la leyenda de confirmación
                            confirmacion.Text = $"🔘Tu solicitud sigue en revisión";
                            //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no)
                            Notificacion.Visibility = ViewStates.Visible;
                            //Damos imagen de positivo a la notificación
                            ImgNotificacion.SetImageResource(Resource.Drawable.Positivo);
                            //Damos titulo de ser correcta
                            TituloNotificacion.Text = "Por favor tenga paciencia";
                        }
                    }
                }
                else if (this.Intent.GetIntExtra("Tipo", 0) == 1)
                {
                    //Iniciamos las restricciónes para definir estados
                    //Recibimos el verdadero o falso del usuario
                    if (resultado != 1)
                    {
                        //Si se ha recibido la solicitud se hara visible recibir y nos data una leyenda con día de llegada de solicitud
                        recibir.Visibility = ViewStates.Visible;
                        recibir.Text = $"🔘Solicitud Registrada {ds.Tables[0].Rows[0]["f_registro"].ToString()}";
                        //Si el resultado es 1 damos los resultados de que fue aceptada la solicitud
                        if (resultado == 2)
                        {
                            //Hacemos visible la confirmación
                            confirmacion.Visibility = ViewStates.Visible;
                            //Damos la leyenda de confirmación
                            confirmacion.Text = $"🔘La solicitud fue Cancelada";
                            //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no)
                            Notificacion.Visibility = ViewStates.Visible;
                            //Damos imagen de positivo a la notificación
                            ImgNotificacion.SetImageResource(Resource.Drawable.Neutro);
                            //Damos titulo de ser correcta
                            TituloNotificacion.Text = "Si así lo descea puede hacer otra solicitud";
                            //Damos una leyenda feliciatando al usuario
                            Observaciones.Text = $"{ds.Tables[0].Rows[0]["observaciones_cancelacion"].ToString()}";
                        }
                        else if (resultado == 5)
                        {
                            //Hacemos visible la confirmación
                            confirmacion.Visibility = ViewStates.Visible;
                            //Damos la leyenda de confirmación
                            confirmacion.Text = $"🔘La solicitud fue Denegada";
                            //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no)
                            Notificacion.Visibility = ViewStates.Visible;
                            //Damos la imagen de denegación de solicitud
                            ImgNotificacion.SetImageResource(Resource.Drawable.Negativo);
                            //Damos titulo de negación
                            TituloNotificacion.Text = "Lastima";
                            //Damos leyenda de notificación de negación
                            LeyendaNotificacion.Text = $"No has sido aceptado para transferencia";
                        }
                        else if (resultado == 6)
                        {
                            //Hacemos visible la confirmación
                            confirmacion.Visibility = ViewStates.Visible;
                            //Damos la leyenda de confirmación
                            confirmacion.Text = $"🔘La solicitud fue Aceptada";
                            //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no)
                            Notificacion.Visibility = ViewStates.Visible;
                            //Damos imagen de positivo a la notificación
                            ImgNotificacion.SetImageResource(Resource.Drawable.Positivo);
                            //Damos titulo de ser correcta
                            TituloNotificacion.Text = "Felicidades!";
                            //Damos una leyenda feliciatando al usuario
                            LeyendaNotificacion.Text = $"Has sido aceptado para transferencia";
                        }
                        else if (resultado == 0)
                        {
                            //Hacemos visible la confirmación
                            confirmacion.Visibility = ViewStates.Visible;
                            //Damos la leyenda de confirmación
                            confirmacion.Text = $"🔘Tu solicitud sigue en revisión";
                            //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no)
                            Notificacion.Visibility = ViewStates.Visible;
                            //Damos imagen de positivo a la notificación
                            ImgNotificacion.SetImageResource(Resource.Drawable.Positivo);
                            //Damos titulo de ser correcta
                            TituloNotificacion.Text = "Por favor tenga paciencia";
                        }
                    }
                }
            }catch (Exception ex)
            {

            }
        }


        private int Llenar()
        {
            clsDatos datos = new clsDatos();
            ds = datos.Iniciar(this.Intent.GetIntExtra("Folio", 0), this.Intent.GetIntExtra("Tipo", 0));
            //Esto es solo para mostrar que se envia, se tiene que borrar

            //Indexados
            TextView Estatus = this.FindViewById<TextView>(Resource.Id.txtInicioEstatusDato);
            TextView Observaciones = this.FindViewById<TextView>(Resource.Id.txtObservacionesDato);
            if (this.Intent.GetIntExtra("Tipo", 0) == 0)
            {
                //Si la solicitud es aceptada como real continuamos
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["solicitud_real"]) == 1)
                {
                    //Si la solicitud es cancelada mandamos esto
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["cancelada"]) == 1)
                    {
                        return 2;
                    }
                    //Si la solicitud es marcada como 0 entonces significa que fue cancelada
                    else if (Convert.ToInt32(ds.Tables[0].Rows[0]["marcada"]) == 0)
                    {
                        //Si la solicitud es validada, mandamos esto
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["validada_dgp"]) == 1)
                        {
                            //Si se ha recibido la solicitud se hara visible recibir y nos data una leyenda con día de llegada de solicitud
                            Estatus.Visibility = ViewStates.Visible;
                            Estatus.Text = "🔘 La solicitud ha sido balidada";
                            return 3;
                        }
                        //Si la solicitud es certificada mandamos esto
                        else if (Convert.ToInt32(ds.Tables[0].Rows[0]["certificada_ur"]) == 1)
                        {
                            //Si se ha recibido la solicitud se hara visible recibir y nos data una leyenda con día de llegada de solicitud
                            Estatus.Visibility = ViewStates.Visible;
                            Estatus.Text = $"🔘La solicitud ha sido Certificada";
                            return 4;
                        }
                        else
                        {
                            return 5;
                        }
                    }
                    //Si la solicitud fue marcada como 1 significa que fue aceptada
                    else if (Convert.ToInt32(ds.Tables[0].Rows[0]["marcada"]) == 1)
                    {
                        return 6;
                    }
                    //Si nada de esto se cumple mandamos esto
                    else { return 1; }
                }
                //Si la solicitud no es aceptada como verdadera mandamos esto
                else
                {
                    return 0;
                }
            }
            else if (this.Intent.GetIntExtra("Tipo", 0) == 1)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["solicitud_real"]) != 0)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["cancelada"]) == 1)
                    {
                        return 2;
                    }
                    
                    else if (Convert.ToInt32(ds.Tables[0].Rows[0]["marcada"]) == 0)
                    {
                        return 5;
                    }
                    else if (Convert.ToInt32(ds.Tables[0].Rows[0]["marcada"]) == 1)
                    {
                        return 6;
                    }
                    else if (Convert.ToInt32(ds.Tables[0].Rows[0]["certificada_ur"]) != null)
                    {
                        //Si se ha recibido la solicitud se hara visible recibir y nos data una leyenda con día de llegada de solicitud
                        Estatus.Visibility = ViewStates.Visible;
                        Estatus.Text = $"🔘La solicitud ha sido Certificada";
                        return 4;
                    }
                    else if (ds.Tables[0].Rows[0]["validada_dgp"] != null)
                    {
                        //Si se ha recibido la solicitud se hara visible recibir y nos data una leyenda con día de llegada de solicitud
                        Estatus.Visibility = ViewStates.Visible;
                        Estatus.Text = "🔘 La solicitud ha sido validada";
                        Observaciones.Text = $"     {ds.Tables[0].Rows[0]["validada_dgp"]}";
                        return 3;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                Toast.MakeText(this, $"Se ha detectado un error, comuniquese con departamento de sistemmas", ToastLength.Long).Show();
                Finish();
                return 10;
            }


        }

        private void Asking_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AcPreguntas));
        }
    }
}