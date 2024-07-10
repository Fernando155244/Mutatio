using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using AlertDialog = Android.App.AlertDialog;

namespace Mutation
{
    [Activity(Label = "Mutatio", Theme = "@style/AppTheme", MainLauncher = true)]
    /*Esta es la pagina principal del programa Mutatio para seguimiento de solicitudes de Cambios y permutas de la SEP.
     En esta pagina tenemos la pagina de login para acceder conforme al RFC y al folio y tipo de solicitud de la petición*/
    public class MainActivity : AppCompatActivity
    {
        //Declaramos la función para conectarnos con la base
        clsDatos datos = new clsDatos();
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            //Declaramos la variable para definir si nos podemos conectar o no
            bool resultado = false;
                //Indexados
                ImageView Logo = this.FindViewById<ImageView>(Resource.Id.imgLoginLogo);
            Button btnLogin = this.FindViewById<Button>(Resource.Id.btnLoginStart);
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spLoginTipoDato);
            
            //Definición de valor
            Logo.SetImageResource(Resource.Drawable.Logo);
            //Función del boton
            btnLogin.Click += BtnLogin_Click ;
            
            //Función automatica del spinner
            LlenarTipo();
            //Iniciamos el intento
            try
            {
                //Mandamos a llamar la función asimetrica Inicial
                resultado = await datos.Conectar();
                //Si nos podemos conectar o no mandara un mensaje con el resultado
                if (resultado)
                {
                    Toast.MakeText(this, $"Conexion exitosa", ToastLength.Long).Show();
                }
            }
            catch//Si no logra conectarse damos aviso de esto
            {
                AlertDialog a1 = new AlertDialog.Builder(this).Create();
                a1.SetTitle("Alerta de red!");
                a1.SetMessage("No hemos podido conectar con el servidor, favor de revisar su conexion WiFi");
                a1.SetButton("Aceptar", btnOK);
                a1.Show();

            }
        }

        private void btnOK(object sender, DialogClickEventArgs e)
        {
        }

        private IDialogInterfaceOnClickListener Aceptar()
        {
            throw new NotImplementedException();
        }

        /*Con esta función al hacer click en el boton manamos a pedir los datos de los text view y el 
* spinner para que nos permita ver si son datos reales y si avanza*/
        private async void BtnLogin_Click(object sender, System.EventArgs e)
        {
            //Indexamos
            TextView RFC = this.FindViewById<TextView>(Resource.Id.txtLoginrfcDato);
            TextView NoFolio = this.FindViewById<TextView>(Resource.Id.txtLoginFolioDato);
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spLoginTipoDato);
            //Iniciamo el intento de conexion
            if (NoFolio.Text == "" || RFC.Text == "")
            {
                AlertDialog a1 = new AlertDialog.Builder(this).Create();
                a1.SetTitle("Atención!");
                a1.SetMessage("Uno o más campos están vacíos, verifique sus datos");
                a1.SetButton("Aceptar", btnOK);
                a1.Show();
            }
            else
            {
                try
                {
                    //Definimos una variable para saber si los datos son correctos o no y la rellenamos con los datos del login
                    bool Log = await datos.Sesion(Convert.ToInt32(NoFolio.Text), RFC.Text, spTipo.SelectedItemPosition);
                    //Si los valores son correctos podremos avanzar
                    if (Log)
                    {
                        //Creamos el intento y mandamso los valores correscpondientes
                        Intent Login = new Intent(this, typeof(AcConfirmación));
                        Login.PutExtra("Folio", Convert.ToInt32(NoFolio.Text));
                        Login.PutExtra("Tipo", spTipo.SelectedItemPosition);
                        //Iniciamos el intento 
                        StartActivity(Login);
                    }//Si no logramos entrar mandamos mensaje de error
                    else
                    {
                        Toast.MakeText(this, $"El RFC y/o el numero de Folio son incorrectos", ToastLength.Long).Show();
                    }

                }//En caso de error mandamos mensaje
                catch (Exception ex)
                {
                    AlertDialog a1 = new AlertDialog.Builder(this).Create();
                    a1.SetTitle("Alerta de Sistema");
                    a1.SetMessage("Ha ocurrido un error en la conexión, revise su conexión WiFi o intente más tarde.");
                    a1.SetButton("OK", btnOK);
                    a1.Show();
                }
            }
        }
        /*Esta función nos permite llenar el spinner para que el usuario pueda ver los tipos de solicitud*/
        private void LlenarTipo()
        {
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spLoginTipoDato);
            ArrayAdapter spInteres = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem);

            //Agregamos los valores de la tabla al arrayAdapter 
            spInteres.Add("Cambios");
            spInteres.Add("Permutas");
            //Ingresamos el adapter al spinner
            spTipo.Adapter = spInteres;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}