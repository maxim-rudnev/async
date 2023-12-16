namespace Async
{
    public partial class Form1 : Form
    {

        Task _task;
        Task _continueWithTask;
        CancellationTokenSource _cts = new CancellationTokenSource();
        bool _throwEx = false;


        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            _throwEx = false;
            _cts = new CancellationTokenSource();

            _task = Task.Run(async () =>
            //_task = new Task(async ()=>
            {
                int i = 0;

                while (true)
                {
                    Thread.Sleep(5000); // Simulating some work

                    //if (i == 2) return;

                    _cts.Token.ThrowIfCancellationRequested();

                    i++;
                    if (_cts.IsCancellationRequested)
                    {
                        return;
                    }

                    if (_throwEx)
                    {
                        throw new Exception("asdfsdaf");
                    }
                }

            },_cts.Token);

            

            _continueWithTask = _task.ContinueWith(async (completedTask) =>
            {
                //try
                //{
                //    await testExFunc();
                //}catch (Exception ex)
                //{
                //    //throw ex;
                //}

                throw new Exception();
            });

            //_task.Start();

            try
            {
                await _continueWithTask;
            }
            catch (AggregateException ex)
            {

            }
        }

        private async Task testExFunc()
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _cts.Cancel();

            _cts2?.Cancel();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _throwEx = true;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                await _task;
            }
            catch (Exception ex)
            {

            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            try
            {
                await _continueWithTask;
            }
            catch (Exception ex)
            {

            }
        }


        Task task = null;

        CancellationTokenSource _cts2;
        private async void button6_Click(object sender, EventArgs e)
        {
            int i = 0;


            _cts2 = new CancellationTokenSource();
            task = Task.Run(async () =>
            {
                Thread.Sleep(5000);

                await Task.Delay(5000);


                while (true)
                {
                    Thread.Sleep(100);
                    i++;

                    if (i == 5)
                        return;

                    _cts2.Token.ThrowIfCancellationRequested();
                    //await Task.Delay(5000);
                }

            }, _cts2.Token).ContinueWith(task =>
            {

            });

            try
            {
                //task.Start();
                //await task;
            }
            catch (Exception ex)
            {

            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            task.ContinueWith(task =>
            {

            });
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            int tid = System.Environment.CurrentManagedThreadId;

            await func();

            //t.ContinueWith(t =>
            //{

            //});

            //await t;
        }


        async Task func()
        {
            int i = 3;
            var t = Task.Run(async () =>{
                int tid = System.Environment.CurrentManagedThreadId;


                for (int a = 0; a < 5; a++)
                {
                    Thread.Sleep(100);
                }

                await Task.Delay(3000);

                for (int a = 0; a < 5; a++)
                {
                    Thread.Sleep(100);
                }

                await Task.Delay(3000);
            });

            i++;
        }
    }
}
