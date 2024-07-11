using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxyPlot.Xamarin.Android;
using System.Runtime.CompilerServices;
using System.Data;
using static Android.Icu.Text.Transliterator;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Microsoft.SqlServer.Server;
using System.Threading.Tasks;

namespace Mutation
{
    [Activity(Label = "AcGraficas")]
    /*En esta pantalla podemos ver en graficas cuantos han sido veneficiados en este sistema*/
    public class AcGraficas : Activity
    {
        clsDatos datos = new clsDatos();

        PlotView Grafica;
        ListView ListaPreguntas;
        Spinner spTipo ;
        Spinner spEstatus ;
        Spinner spReferencias ;
        Switch swcertificadas;
        Switch swCanelada;
        Button Actualizar;
        //Variables de definición
        int certificada;
        int cancelar;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            /*Variables para pruebas*/
            DataSet ds;
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Graficas);

            //Index y definición de logo
            ImageView imgLogo = this.FindViewById<ImageView>(Resource.Id.Logo);
            imgLogo.SetImageResource(Resource.Drawable.Logo);

            //Indexados
            Grafica = this.FindViewById<PlotView>(Resource.Id.GrafGraficas);
            ListaPreguntas = this.FindViewById<ListView>(Resource.Id.lisGraficas);
            spTipo = this.FindViewById<Spinner>(Resource.Id.spGraficasTipoSolicitud);
            spEstatus = this.FindViewById<Spinner>(Resource.Id.spGraficasEstatus);
            spReferencias = this.FindViewById<Spinner>(Resource.Id.spGraficasReferencia);
            swcertificadas = this.FindViewById<Switch>(Resource.Id.swGraficasCertificadas);
            swCanelada = this.FindViewById<Switch>(Resource.Id.swyGraficasCanceladas);
            Actualizar = this.FindViewById<Button>(Resource.Id.btnActualizar);
            Actualizar.Click += Actualizar_Click;
            

            //Función automatica del spinner
            LlenarTipo();
            LlenarEstatus();
            LlenarGraficas();

            //Definimos los valores segun lo que se decida en los switch
            if (swcertificadas.Selected)
            {
                certificada = 1;
            }
            else
            {
                certificada = 0;
            }
            if (swCanelada.Selected)
            {
                cancelar = 1;
            }
            else
            {
                cancelar = 0;
            }
            //Iniciamos el intento
            try
            {
                //Mandamos a llamar la función acincrona
                ds = datos.graficadora(spReferencias.SelectedItemPosition, spTipo.SelectedItemPosition, certificada, cancelar, spEstatus.SelectedItemPosition);

                //Mandamos a llenar la lista y grafica
                ListaPreguntas.Adapter = new RellenarGraficas(this, ds, spReferencias.SelectedItemPosition, spReferencias.SelectedItemPosition, spTipo.SelectedItemPosition, certificada, cancelar, spEstatus.SelectedItemPosition);
                Grafica.Model = Estados(ds);
            }
            catch (Exception ex)//En  caso de fallar se manda un mensaje de alerta
            {
                AlertDialog a1 = new AlertDialog.Builder(this).Create();
                a1.SetTitle("Alerta!");
                a1.SetMessage($"Se ha detectado un error, comuníquese con departamento de sistemas\n{ex}");
                a1.SetButton("Aceptar", btnOK);
                a1.Show();
            }
        }
        private void btnOK(object sender, DialogClickEventArgs e)
        {
            Finish();
        }

        private async void Actualizar_Click(object sender, EventArgs e)
        {

            //Variables de definición
            int certificada;
            int cancelar;

            //Definimos los valores segun lo que se decida en los switch
            if (swcertificadas.Selected)
            {
                certificada = 1;
            }
            else
            {
                certificada = 0;
            }
            if (swCanelada.Selected)
            {
                cancelar = 1;
            }
            else
            {
                cancelar = 0;
            }
            //Iniciamos el intento
            try
            {
                //Mandamos a llamar la función acincrona
                DataSet ds = datos.graficadora(spReferencias.SelectedItemPosition, spTipo.SelectedItemPosition, certificada, cancelar, spEstatus.SelectedItemPosition);

                //Mandamos a llenar la lista y grafica
                ListaPreguntas.Adapter = new RellenarGraficas(this, ds, spReferencias.SelectedItemPosition, spReferencias.SelectedItemPosition, spTipo.SelectedItemPosition, certificada, cancelar, spEstatus.SelectedItemPosition);
                Grafica.Model = Estados(ds);
            }
            catch (Exception ex)//En  caso de fallar se manda un mensaje de alerta
            {
                AlertDialog a1 = new AlertDialog.Builder(this).Create();
                a1.SetTitle("Alerta!");
                a1.SetMessage($"Se ha detectado un error, comuníquese con departamento de sistemas\n{ex}");
                a1.SetButton("Aceptar", btnOK);
                a1.Show();
            }
        }

        private void LlenarTipo()
        {
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spGraficasTipoSolicitud);
            ArrayAdapter spInteres = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem);

            //Agregamos los valores de la tabla al arrayAdapter 
            spInteres.Add("Todos");
            spInteres.Add("Cambios");
            spInteres.Add("Permutas");
            //Ingresamos el adapter al spinner
            spTipo.Adapter = spInteres;
        }
        private void LlenarEstatus()
        {
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spGraficasEstatus);
            ArrayAdapter spInteres = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem);

            //Agregamos los valores de la tabla al arrayAdapter 
            spInteres.Add("Todos");
            spInteres.Add("R");
            spInteres.Add("C");
            //Ingresamos el adapter al spinner
            spTipo.Adapter = spInteres;
        }
        private void LlenarGraficas()
        {
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spGraficasReferencia);
            ArrayAdapter spInteres = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem);

            //Agregamos los valores de la tabla al arrayAdapter 
            spInteres.Add("Nivel Educativo");
            spInteres.Add("Estado");
            //Ingresamos el adapter al spinner
            spTipo.Adapter = spInteres;
        }
        private PlotModel Estados(DataSet ds)
        {
            Spinner spReferencias = this.FindViewById<Spinner>(Resource.Id.spGraficasReferencia);
            CategoryAxis ejex = new CategoryAxis();
            LinearAxis ejey = new LinearAxis();
            //al valor x le damos los nombres de los valores
            ejex.Position = AxisPosition.Bottom;
            //Le damos valores minimo y maximo
            ejey.Minimum = 0;
            ejey.Maximum = 0;
            ejey.Position = AxisPosition.Left;
            //Creamos los valores de las graficas
            ColumnSeries s1 = new ColumnSeries();
            //Creamos el modelo de graficas
            PlotModel m1 = new PlotModel();
            try
            {

                if (spReferencias.SelectedItemPosition == 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ejex.Labels.Add(ds.Tables[0].Rows[i]["nivel"].ToString());
                        s1.Items.Add(new ColumnItem(Convert.ToInt32(ds.Tables[0].Rows[i]["Cant"].ToString())));
                        if (Convert.ToInt32(ds.Tables[0].Rows[i]["Cant"].ToString()) > ejey.Maximum)
                        {
                            ejey.Maximum = Convert.ToInt32(ds.Tables[0].Rows[i]["Cant"].ToString());
                        }
                    }
                    
                    //Le damos titulo a la grafica
                    m1.Title = "Productos";
                    //Le damos los formatos de x e y
                    m1.Axes.Add(ejey);
                    m1.Axes.Add(ejex);
                    //Le damos los valores a la grafica
                    m1.Series.Add(s1);
                    //Regresamos los valores ingresados 
                    return m1;
                }
                else
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ejex.Labels.Add(ds.Tables[0].Rows[i]["nombre"].ToString());
                        s1.Items.Add(new ColumnItem(Convert.ToInt32(ds.Tables[0].Rows[i]["Cant"].ToString())));
                        if (Convert.ToInt32(ds.Tables[0].Rows[i]["Cant"].ToString()) > ejey.Maximum)
                        {
                            ejey.Maximum = Convert.ToInt32(ds.Tables[0].Rows[i]["Cant"].ToString());
                        }
                    }
                    //Creamos el modelo de graficas
                    //PlotModel m1 = new PlotModel();
                    //Le damos titulo a la grafica
                    m1.Title = "Productos";
                    //Le damos los formatos de x e y
                    m1.Axes.Add(ejey);
                    m1.Axes.Add(ejex);
                    //Le damos los valores a la grafica
                    m1.Series.Add(s1);
                    //Regresamos los valores ingresados 
                    return m1;
                }
            }
            catch {
                m1 =null;
                return m1;
            }
        }
    }

    internal class RellenarGraficas : BaseAdapter
    {
        private AcGraficas acGraficas;
        private DataSet ds;
        private int Tipo;
        private int selectedItemPosition1;
        private int certificada;
        private int cancelar;
        private int selectedItemPosition2;


        public RellenarGraficas(AcGraficas acGraficas, DataSet ds, int Tipo, int selectedItemPosition, int selectedItemPosition1, int certificada, int cancelar, int selectedItemPosition2)
        {
            this.acGraficas = acGraficas;
            this.ds = ds;
            this.Tipo = Tipo;
            this.selectedItemPosition1 = selectedItemPosition1;
            this.certificada = certificada;
            this.cancelar = cancelar;
            this.selectedItemPosition2 = selectedItemPosition2;
        }

        public override int Count
        {
            
            get
            {
                    return ds.Tables[0].Rows.Count;
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
            clsDatos Datos = new clsDatos();
            try
            {
                
                //Declaración de vista
                if (vista == null)
                {
                    //Aqui aparte de declarar los valores basicos declaramos el uso del diseño TabEmpresa para la fila
                    vista = acGraficas.LayoutInflater.Inflate(Resource.Layout.ModGraficos, null);
                }
                TextView Estado = vista.FindViewById<TextView>(Resource.Id.txtTabEstado);
                TextView Numeros = vista.FindViewById<TextView>(Resource.Id.txtTabNumeros);
                if (Tipo == 0)
                {
                    Estado.Text = ds.Tables[0].Rows[position]["Nivel"].ToString();
                    Numeros.Text = ds.Tables[0].Rows[position]["Cant"].ToString();
                }
                else if (Tipo == 1)
                {
                    Estado.Text = ds.Tables[0].Rows[position]["nombre"].ToString();
                    Numeros.Text = ds.Tables[0].Rows[position]["Cant"].ToString();
                }
                return vista;

            }
            catch (Exception ex)
            {
                try
                {
                    //Declaración de vista
                    if (vista == null)
                    {
                        //Aqui aparte de declarar los valores basicos declaramos el uso del diseño TabEmpresa para la fila
                        vista = acGraficas.LayoutInflater.Inflate(Resource.Layout.ModGraficos, null);
                    }
                    TextView Estado = vista.FindViewById<TextView>(Resource.Id.txtTabEstado);
                    TextView Numeros = vista.FindViewById<TextView>(Resource.Id.txtTabNumeros);
                    if (Tipo == 0)
                    {
                        Estado.Text = ds.Tables[0].Rows[position]["Nivel"].ToString();
                        Numeros.Text = ds.Tables[0].Rows[position]["Cant"].ToString();
                    }
                    else if (Tipo == 1)
                    {
                        Estado.Text = ds.Tables[0].Rows[position]["nombre"].ToString();
                        Numeros.Text = ds.Tables[0].Rows[position]["Cant"].ToString();
                    }
                    return vista;
                }
                catch (Exception x)
                {
                    ds = Datos.graficadora(selectedItemPosition1, certificada, cancelar, selectedItemPosition2, Tipo);
                    //Toast.MakeText(this,$"Error en los datos, intente de nuevo {ex}", ToastLength.Long).Show();
                    AlertDialog a1 = new AlertDialog.Builder(this.acGraficas).Create();
                    a1.SetTitle("Alerta!");
                    a1.SetMessage($"Error en los datos, intente de nuevo {x}");
                    a1.SetButton("OK", btnOK);
                    a1.Show();
                    return null;
                }

            }
        }

        private void btnOK(object sender, DialogClickEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
    
}