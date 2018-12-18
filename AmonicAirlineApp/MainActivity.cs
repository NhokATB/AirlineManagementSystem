using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace AmonicAirlineApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button btnLogin;
        private Button btnLoginWithGuest;
        private EditText edtEmail;
        private EditText edtPassword;
        private Button btnExit;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            ImageView imgLogo = this.FindViewById<ImageView>(Resource.Id.imgLogo);
            imgLogo.SetImageResource(Resource.Drawable.WSC2017_TP09_color);

            edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);
            edtPassword = FindViewById<EditText>(Resource.Id.edtPassword);

            btnLoginWithGuest = this.FindViewById<Button>(Resource.Id.btnLoginWithGuest);
            btnLoginWithGuest.Click += BtnLoginWithGuest_Click;

            btnLogin = this.FindViewById<Button>(Resource.Id.btnLogin);
            btnLogin.Click += BtnLogin_Click;

            btnExit = this.FindViewById<Button>(Resource.Id.btnExit);
            btnExit.Click += BtnExit_Click;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }

        public override void OnBackPressed()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = dialog.Create();

            if (edtEmail.Text.Trim() == "")
            {
                alert.SetTitle("Message");
                alert.SetMessage("Email was required");
                alert.SetButton("OK", (c, ev) =>
                {

                });
                alert.Show();
                return;
            }

            if (edtPassword.Text.Trim() == "")
            {
                alert.SetTitle("Message");
                alert.SetMessage("Password was required");
                alert.SetButton("OK", (c, ev) =>
                {

                });
                alert.Show();
                return;
            }

            if (edtEmail.Text == "admin@amonic.com" && edtPassword.Text == "123")
            {
                Intent intent = new Intent(this, typeof(AdminActivity));
                StartActivity(intent);
            }
            else if (edtEmail.Text == "manager@amonic.com" && edtPassword.Text == "123")
            {
                Intent intent = new Intent(this, typeof(ManagerActivity));
                StartActivity(intent);
            }
            else
            {
                alert.SetTitle("Message");
                alert.SetMessage("Email or password not correct");
                alert.SetButton("OK", (c, ev) =>
                {

                });
                alert.Show();
                return;
            }
        }
        private async void Login()
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = dialog.Create();

            var pass = GetMd5(edtPassword.Text);

            var client = new HttpClient();
            var url = $"http://10.0.2.2:61757/Api/Users/login?email={edtEmail.Text}&password={pass}";

            var response = await client.GetAsync(url);

            if (response != null)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                if(json.Contains("True"))
                {
                    if (json.Contains("Administrator"))
                    {
                        Intent intent = new Intent(this, typeof(AdminActivity));
                        StartActivity(intent);
                    }
                    else if (json.Contains("Manager"))
                    {
                        Intent intent = new Intent(this, typeof(ManagerActivity));
                        StartActivity(intent);
                    }
                }
                else
                {
                    alert.SetTitle("Message");
                    alert.SetMessage("Email or password not correct");
                    alert.SetButton("OK", (c, ev) =>
                    {

                    });
                    alert.Show();
                }
            }
            else
            {
                alert.SetTitle("Message");
                alert.SetMessage("Response not available");
                alert.SetButton("OK", (c, ev) =>
                {

                });
                alert.Show();
            }
        }
        private void BtnLoginWithGuest_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GuestActivity));
            StartActivity(intent);
        }

        public static string GetMd5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            var buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(buffer).Replace("-", "");
        }
    }
}

