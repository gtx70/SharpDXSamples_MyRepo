using System;
using System.ComponentModel;
using System.Text;
using SharpDX.Windows;

namespace HelloTriangle
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var form = new RenderForm("Hello Triangle")
            {
                Width = 1280,
                Height = 800,
            };
            form.AllowUserResizing = false;
            form.Show();

            using (var app = new HelloTriangle())
            {
                app.Initialize(form);

                using (var loop = new RenderLoop(form))
                {

                    StringBuilder stringBuilder = new StringBuilder();
                    var watch = new System.Diagnostics.Stopwatch();
                    double frequency = System.Diagnostics.Stopwatch.Frequency;
                    double elapasedTime = 0;
                    watch.Start();

                    while (loop.NextFrame())
                    {
                        watch.Stop();
                        double fps = frequency / watch.ElapsedTicks;
                        elapasedTime += watch.Elapsed.TotalMilliseconds;
                        // FPS counter interval = 500ms
                        if (elapasedTime > 0)
                        {
                            elapasedTime = 0;
                            stringBuilder.Clear();
                            stringBuilder.Append($"Hello Triangle {fps.ToString("f0")} FPS");
                            form.Text = stringBuilder.ToString();
                        }
                        watch.Restart();

                        app.Update();
                        app.Render();
                    }
                }
            }
        }
    }
}
