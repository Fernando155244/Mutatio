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
        DataSet ds = new DataSet();
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Inicio);
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
            TextView Certificada = this.FindViewById<TextView>(Resource.Id.txtInicioCertificada);
            Asking.Click += Asking_Click;


            datSolicitud.Text = $"Solicitud de Permuta Numero {ds.Tables[0].Rows[0]["id"].ToString()}";
            try
            {
                
                if (this.Intent.GetIntExtra("Tipo", 0) == 0)
                {
                    //Con esto ingresamos los dtos de la consulta
                    datSolicitud.Text = $"Solicitud de Cambios Numero: {this.Intent.GetIntExtra("Folio", 0)}";
                    Notificacion.Visibility = ViewStates.Invisible;
                    //Con esto ingresamos el nombre del usuario
                    datosSolicitante.Text = $"Del trabajador: {ds.Tables[0].Rows[0]["paterno"].ToString()} {ds.Tables[0].Rows[0]["materno"].ToString()} {ds.Tables[0].Rows[0]["nombre"].ToString()}";
                    //Iniciamos las restricciones para definir estados
                    //Recibimos el verdadero o falso del usuario
                    /*
                          Orden
                          real
                          certificada
                          validada
                          marcada
                          cancelada
                         */
                    //Si se ha recibido la solicitud se hará visible recibir y nos data una leyenda con día de llegada de solicitud
                    recibir.Visibility = ViewStates.Visible;
                    recibir.Text = $"🔘Solicitud Registrada {ds.Tables[0].Rows[0]["f_registro"].ToString()}";
                    //Si el resultado es 1 damos los resultados de que fue aceptada la solicitud
                    if (resultado == 2)
                    {
                        //Función de conexión con el servidor
                        clsDatos d = new clsDatos();
                        //Enviamos valores para saber fecha de cancelación
                        DataSet dsC = d.Cancelacion(this.Intent.GetIntExtra("Folio", 0), this.Intent.GetIntExtra("Tipo", 0));


                        //Hacemos visible la cancelación de la solicitud
                        confirmacion.Visibility = ViewStates.Visible;
                        //Damos la leyenda de cancelación
                        confirmacion.Text = $"🔘Su Solicitud de Cambio Fue Cancelada\nFecha de cancelación: {dsC.Tables[0].Rows[0]["f_cancelacion"].ToString()}";
                        //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no o cancelamos)
                        Notificacion.Visibility = ViewStates.Visible;
                        //Damos imagen de cancelado a la notificación
                        ImgNotificacion.SetImageResource(Resource.Drawable.Neutro);
                        //Damos titulo de concejo por cancelar
                        TituloNotificacion.Text = $"La solicitud fue cancelada";
                        //Damos una leyenda con las obsercaciones al cancelar al usuario
                        Observaciones.Text += $"{ds.Tables[0].Rows[0]["observaciones_cancelacion"].ToString()}";
                    }
                    else if (resultado == 0)
                    {
                        if (ds.Tables[0].Rows[0]["estatus"].ToString() == "C" || ds.Tables[0].Rows[0]["estatus"].ToString() == "c" || ds.Tables[0].Rows[0]["estatus"].ToString() == "Vacio")
                        {
                            //Hacemos visible la Negativa
                            confirmacion.Visibility = ViewStates.Visible;
                            //Damos la leyenda de necación de solicitud
                            confirmacion.Text = $"🔘Su Solicitud de Cambio no fue Beneficiada";
                            //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no o cancelamos)
                            Notificacion.Visibility = ViewStates.Visible;
                            //Damos la imagen de denegación de solicitud
                            ImgNotificacion.SetImageResource(Resource.Drawable.Negativo);
                            //Damos titulo de negación
                            TituloNotificacion.Text = "";
                            Observaciones.Text += $"{ds.Tables[0].Rows[0]["obsevaciones"].ToString()}";
                            //Damos leyenda de notificación de negación
                            LeyendaNotificacion.Text = $"Su solicitud de cambio no fue beneficiada";
                        }
                        else if (ds.Tables[0].Rows[0]["estatus"].ToString() == "R" || ds.Tables[0].Rows[0]["estatus"].ToString() == "r")
                        {
                            //Hacemos visible la Negativa
                            confirmacion.Visibility = ViewStates.Visible;
                            //Damos la leyenda de necación de solicitud
                            confirmacion.Text = $"🔘Su Solicitud de Cambio fue rechazada por tu estado ";
                            //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no o cancelamos)
                            Notificacion.Visibility = ViewStates.Visible;
                            //Damos la imagen de denegación de solicitud
                            ImgNotificacion.SetImageResource(Resource.Drawable.Negativo);
                            //Damos titulo de negación
                            TituloNotificacion.Text = "";
                            Observaciones.Text += $"Tu solicitud fue rechazada por: {ds.Tables[0].Rows[0]["obsevaciones"].ToString()}";
                            //Damos leyenda de notificación de negación
                            LeyendaNotificacion.Text = $"Su solicitud de cambio fue Rechazada";
                            Certificada.Visibility = ViewStates.Visible;
                            Certificada.Text = $"🔘La solicitud de cambios ha sido Rechazada";
                        }
                        else
                        {
                            //Hacemos visible la Negativa
                            confirmacion.Visibility = ViewStates.Visible;
                            //Damos la leyenda de negación de solicitud
                            confirmacion.Text = $"🔘Su Solicitud de Cambio no fue Beneficiada";
                            //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no o cancelamos)
                            Notificacion.Visibility = ViewStates.Visible;
                            //Damos la imagen de denegación de solicitud
                            ImgNotificacion.SetImageResource(Resource.Drawable.Negativo);
                            //Damos titulo de negación
                            TituloNotificacion.Text = "";
                            Observaciones.Text += $"{ds.Tables[0].Rows[0]["obsevaciones"].ToString()}";
                            //Damos leyenda de notificación de negación
                            LeyendaNotificacion.Text = $"Su solicitud de cambio no fue beneficiada";
                        }

                    }
                    else if (resultado == 1)
                    {
                        //Hacemos visible la confirmación
                        confirmacion.Visibility = ViewStates.Visible;
                        //Damos la leyenda de confirmación
                        confirmacion.Text = $"🔘Su Solicitud de Cambio fue Beneficiada";
                        //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no)
                        Notificacion.Visibility = ViewStates.Visible;
                        //Damos imagen de positivo a la notificación
                        ImgNotificacion.SetImageResource(Resource.Drawable.Positivo);
                        //Damos titulo de ser correcta
                        TituloNotificacion.Text = "Felicidades!";
                        //Damos una leyenda felicitando al usuario
                        LeyendaNotificacion.Text = $"Su solicitud de cambio fue Beneficiada {ds.Tables[0].Rows[0]["obsevaciones"].ToString()}";
                    }
                }
                else if (this.Intent.GetIntExtra("Tipo", 0) == 1)
                {
                    //Con esto ingresamos los dtos de la consulta
                    datSolicitud.Text = $"Solicitud de Permuta Numero: {this.Intent.GetIntExtra("Folio", 0)}";
                    Notificacion.Visibility = ViewStates.Invisible;
                    //Con esto ingresamos el nombre del usuario
                    datosSolicitante.Text = $"Del trabajador: {ds.Tables[0].Rows[0]["paterno"].ToString()} {ds.Tables[0].Rows[0]["materno"].ToString()} {ds.Tables[0].Rows[0]["nombres"].ToString()}";
                    //Iniciamos las restricciones para definir estados
                    //Recibimos el verdadero o falso del usuario
                    //Si se ha recibido la solicitud se hará visible recibir y nos data una leyenda con día de llegada de solicitud
                    recibir.Visibility = ViewStates.Visible;
                    recibir.Text = $"🔘Solicitud Registrada {ds.Tables[0].Rows[0]["f_registro"].ToString()}";
                    //Si el resultado es 1 damos los resultados de que fue aceptada la solicitud
                    if (resultado == 2)
                    {
                        //Función de conexión con el servidor
                        clsDatos d = new clsDatos();
                        //Enviamos valores para saber fecha de cancelación
                        DataSet dsC = d.Cancelacion(this.Intent.GetIntExtra("Folio", 0), this.Intent.GetIntExtra("Tipo", 0));


                        //Hacemos visible la cancelación de la solicitud
                        confirmacion.Visibility = ViewStates.Visible;
                        //Damos la leyenda de cancelación
                        confirmacion.Text = $"🔘Su Solicitud de Permuta Fue Cancelada\nFecha de cancelación: {dsC.Tables[0].Rows[0]["f_cancelacion"].ToString()}";
                        //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no o cancelamos)
                        Notificacion.Visibility = ViewStates.Visible;
                        //Damos imagen de cancelado a la notificación
                        ImgNotificacion.SetImageResource(Resource.Drawable.Neutro);
                        //Damos titulo de concejo por cancelar
                        TituloNotificacion.Text = $"La solicitud fue cancelada";
                        //Damos una leyenda con las observaciones al cancelar al usuario
                        Observaciones.Text += $"{ds.Tables[0].Rows[0]["observaciones_cancelacion"].ToString()}";
                    }
                    else if (resultado == 0)
                    {
                        if (ds.Tables[0].Rows[0]["estatus"].ToString() == "C" || ds.Tables[0].Rows[0]["estatus"].ToString() == "c" || ds.Tables[0].Rows[0]["estatus"].ToString() == "Vacio")
                        {
                            //Hacemos visible la Negativa
                            confirmacion.Visibility = ViewStates.Visible;
                            //Damos la leyenda de negación de solicitud
                            confirmacion.Text = $"🔘Su Solicitud de permuta no fue Beneficiada";
                            //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no o cancelamos)
                            Notificacion.Visibility = ViewStates.Visible;
                            //Damos la imagen de denegación de solicitud
                            ImgNotificacion.SetImageResource(Resource.Drawable.Negativo);
                            //Damos titulo de negación
                            TituloNotificacion.Text = "";
                            Observaciones.Text += $"{ds.Tables[0].Rows[0]["obsevaciones"].ToString()}";
                            //Damos leyenda de notificación de negación
                            LeyendaNotificacion.Text = $"Su solicitud de permuta no fue beneficiada";
                        }
                        else if (ds.Tables[0].Rows[0]["estatus"].ToString() == "R" || ds.Tables[0].Rows[0]["estatus"].ToString() == "r")
                        {
                            //Hacemos visible la Negativa
                            confirmacion.Visibility = ViewStates.Visible;
                            //Damos la leyenda de necación de solicitud
                            confirmacion.Text = $"🔘Su Solicitud de permuta fue rechazada por tu estado ";
                            //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no o cancelamos)
                            Notificacion.Visibility = ViewStates.Visible;
                            //Damos la imagen de denegación de solicitud
                            ImgNotificacion.SetImageResource(Resource.Drawable.Negativo);
                            //Damos titulo de negación
                            TituloNotificacion.Text = "";
                            Observaciones.Text += $"Tu solicitud fue rechazada por: {ds.Tables[0].Rows[0]["obsevaciones"].ToString()}";
                            //Damos leyenda de notificación de negación
                            LeyendaNotificacion.Text = $"Su solicitud de permuta fue Rechazada";
                            Certificada.Visibility = ViewStates.Visible;
                            Certificada.Text = $"🔘La solicitud de permuta ha sido Rechazada";
                        }
                        else
                        {
                            //Hacemos visible la Negativa
                            confirmacion.Visibility = ViewStates.Visible;
                            //Damos la leyenda de necación de solicitud
                            confirmacion.Text = $"🔘Su Solicitud de permuta no fue Beneficiada";
                            //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no o cancelamos)
                            Notificacion.Visibility = ViewStates.Visible;
                            //Damos la imagen de denegación de solicitud
                            ImgNotificacion.SetImageResource(Resource.Drawable.Negativo);
                            //Damos titulo de negación
                            TituloNotificacion.Text = "";
                            Observaciones.Text += $"{ds.Tables[0].Rows[0]["obsevaciones"].ToString()}";
                            //Damos leyenda de notificación de negación
                            LeyendaNotificacion.Text = $"Su solicitud de permuta no fue beneficiada";
                        }
                    }
                    else if (resultado == 1)
                    {
                        //Hacemos visible la confirmación
                        confirmacion.Visibility = ViewStates.Visible;
                        //Damos la leyenda de confirmación
                        confirmacion.Text = $"🔘Su Solicitud de Permuta fue beneficiada";
                        //Hacemos visible la notificación (El lineart layout que dice si fuimos aceptados o no)
                        Notificacion.Visibility = ViewStates.Visible;
                        //Damos imagen de positivo a la notificación
                        ImgNotificacion.SetImageResource(Resource.Drawable.Positivo);
                        //Damos titulo de ser correcta
                        TituloNotificacion.Text = "Felicidades!";
                        //Damos una leyenda feliciatando al usuario
                        LeyendaNotificacion.Text = $"Su solicitud de permuta fue beneficiada {ds.Tables[0].Rows[0]["obsevaciones"].ToString()}";
                    }
                    else if (resultado == 10)
                    {
                        Toast.MakeText(this, $"Error detectado!", ToastLength.Long).Show();
                    }
                }
            }
            catch (Exception ex)
            {
                AlertDialog a1 = new AlertDialog.Builder(this).Create();
                a1.SetTitle("Alerta!");
                a1.SetMessage("Ha ocurrido un error en el sistema, intente más tarde.");
                a1.SetButton("Aceptar", btnOK);
                a1.Show();
            }
        }

        private void Asking_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(AcPreguntas));
        }

        private void btnOK(object sender, DialogClickEventArgs e)
        {
            Finish();
        }

        private int Llenar()
        {
            /*
             * Orden
             real
            certificada
            validada
            marcada
            cancelada
             */
            clsDatos datos = new clsDatos();
            try
            {
                ds = datos.Iniciar(this.Intent.GetIntExtra("Folio", 0), this.Intent.GetIntExtra("Tipo", 0));

                //Indexados
                TextView Observaciones = this.FindViewById<TextView>(Resource.Id.txtObservacionesDato);
                TextView Real = this.FindViewById<TextView>(Resource.Id.txtInicioRegistrado);
                TextView Certificada = this.FindViewById<TextView>(Resource.Id.txtInicioCertificada);
                TextView Validada = this.FindViewById<TextView>(Resource.Id.txtInicioValidada);
                if (this.Intent.GetIntExtra("Tipo", 0) == 0)
                {
                    //Si la solicitud es aceptada como real continuamos
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["solicitud_real"]) == 1)
                    {
                        Real.Visibility = ViewStates.Visible;
                        Real.Text = $"🔘La solicitud de cambio ha sido Verificada {ds.Tables[0].Rows[0]["f_registro"].ToString()}";
                    }
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["certificada_ur"]) == 1)
                    {
                        Certificada.Visibility = ViewStates.Visible;
                        Certificada.Text = $"🔘La solicitud de cambio ha sido Certificada {ds.Tables[0].Rows[0]["f_cetificada_ur"].ToString()}";
                        //Si la solicitud es marcada como 0 entonces significa que fue cancelada
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["cancelada"]) == 1)
                        {
                            return 2;
                        }
                        else if (Convert.ToInt32(ds.Tables[0].Rows[0]["marcada"]) == 0)
                        {

                            return 0;
                        }
                        //Si la solicitud fue marcada como 1 significa que fue aceptada
                        else if (Convert.ToInt32(ds.Tables[0].Rows[0]["marcada"]) == 1)
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        Certificada.Visibility = ViewStates.Visible;
                        Certificada.Text = $"🔘La solicitud de cambio aún no ha sido certificada";
                    }
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["validada_dgp"]) == 1)
                    {
                        Validada.Visibility = ViewStates.Visible;
                        Validada.Text = $"🔘La solicitud de permuta ha sido Validada{ds.Tables[0].Rows[0]["f_validacion_dgp"].ToString()}\n{ds.Tables[0].Rows[0]["observaciones_dgp"].ToString()}";
                    }
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["cancelada"]) == 1)
                    {
                        return 2;
                    }
                    //Si nada de esto se cumple mandamos esto
                    else { return 10; }
                }
                else if (this.Intent.GetIntExtra("Tipo", 0) == 1)
                {
                    //Si la solicitud es aceptada como real continuamos
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["solicitud_real"]) == 1)
                    {
                        Real.Visibility = ViewStates.Visible;
                        Real.Text = $"🔘La solicitud de permuta ha sido Verificada {ds.Tables[0].Rows[0]["f_registro"].ToString()}";
                    }
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["certificada_ur"]) == 1)
                    {
                        Certificada.Visibility = ViewStates.Visible;
                        Certificada.Text = $"🔘La solicitud de permuta ha sido Certificada {ds.Tables[0].Rows[0]["f_certificacion_ur"].ToString()}\n{ds.Tables[0].Rows[0]["observacion_ur"].ToString()}";
                        if (Convert.ToInt32(ds.Tables[0].Rows[0]["cancelada"]) == 1)
                        {
                            return 2;
                        }
                        else if (Convert.ToInt32(ds.Tables[0].Rows[0]["marcada"]) == 0)
                        {

                            return 0;
                        }
                        //Si la solicitud fue marcada como 1 significa que fue aceptada
                        else if (Convert.ToInt32(ds.Tables[0].Rows[0]["marcada"]) == 1)
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        Certificada.Visibility = ViewStates.Visible;
                        Certificada.Text = $"🔘La solicitud de permuta aun no ha cido certificada";
                    }
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["validada_dgp"]) == 1)
                    {
                        Validada.Visibility = ViewStates.Visible;
                        Validada.Text = $"🔘La solicitud de permuta ha sido Validada{ds.Tables[0].Rows[0]["f_validacion_dgp"].ToString()}\n{ds.Tables[0].Rows[0]["observaciones_dgp"].ToString()}";
                    }
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["cancelada"]) == 1)
                    {
                        return 2;
                    }
                    //Si la solicitud es marcada como 0 entonces significa que fue cancelada
                    //Si nada de esto se cumple mandamos esto
                    else { return 10; }

                }
                else
                {
                    Toast.MakeText(this, $"Se ha detectado un error, comuníquese con departamento de sistemas", ToastLength.Long).Show();
                    Finish();
                    return 10;
                }
            }
            catch (Exception ex)
            {
                AlertDialog a1 = new AlertDialog.Builder(this).Create();
                a1.SetTitle("Alerta!");
                a1.SetMessage("Ha ocurrido un error en el sistema, intente más tarde.");
                a1.SetButton("Aceptar", btnOK);
                a1.Show();
            }

            return 10;

        }
    }
}
