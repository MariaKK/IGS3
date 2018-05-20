using System;
using System.Drawing;
using System.Windows.Forms;
using CSat;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace CSatExamples
{
    public struct CarInfo// структура для хранения характеристик авто
    {
        public float speed;// скорость
        public float up, down, max;//изменение скорости
        public int getsprites;// кол-во собранных штук

    }

    class Game5 : GameWindow
    {
        private int _oldMouseX, _oldMouseY; //координаты мыши до изменения
        static Random random = new Random(); //
        Camera cam = new Camera();//
        Font font = new Font(FontFamily.GenericSansSerif, 24.0f);//
        OpenTK.Graphics.TextPrinter text = new OpenTK.Graphics.TextPrinter();//
        Sky skybox = new Sky("skybox");//
        Mesh car;//
        Mesh[] mesprite = new Mesh[5];//
        Mesh[] meslock = new Mesh[5];//
        Object2D groundPlane = new Object2D("ground");//
        CarInfo carinfo;//
        Node world = new Node("world");//
        Light light = new Light("light");//

        public Game5(int width, int height) : base(width, height, OpenTK.Graphics.GraphicsMode.Default, "Горизонталь")
        {
        }
        /// <summary>Load resources here.</summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(System.Drawing.Color.Blue);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            GL.Enable(EnableCap.CullFace);
            GL.ShadeModel(ShadingModel.Smooth);

            Light.Enable();
            light.Position = new Vector3(10, 100, 10);
            light.UpdateColor();
            light.SetLight(true);

            world.Add(light); 
            skybox.LoadSkybox("sky/sky_", "jpg", 100);
            world.Add(skybox);

            carinfo.up = 0.2f;
            carinfo.down = 0.1f;
            carinfo.max = 5;

            car = new ObjModel("car", "car.obj", 2, 2, 2);
            car.FixRotation.Y = -90;
            car.Rotation.Y = 90;

            mesprite[0] = new ObjModel("sprite1", "ball.obj", 1, 1, 1);
            SetupSprite(mesprite[0]);
            mesprite[1] = new ObjModel("sprite2", "ball.obj", 1, 1, 1);
            SetupSprite(mesprite[1]);
            mesprite[2] = new ObjModel("sprite3", "ball.obj", 1, 1, 1);
            SetupSprite(mesprite[2]);
            mesprite[3] = new ObjModel("sprite4", "ball.obj", 1, 1, 1);
            SetupSprite(mesprite[3]);
            mesprite[4] = new ObjModel("sprite5", "ball.obj", 1, 1, 1);
            SetupSprite(mesprite[4]);

            meslock[0] = new ObjModel("lock1", "truck.obj", 1, 1, 1);
            SetupLock(meslock[0]);
            meslock[1] = new ObjModel("lock2", "truck.obj", 1, 1, 1);
            SetupLock(meslock[1]);
            meslock[2] = new ObjModel("lock3", "truck.obj", 1, 1, 1);
            SetupLock(meslock[2]);
            meslock[3] = new ObjModel("lock4", "truck.obj", 1, 1, 1);
            SetupLock(meslock[3]);
            meslock[4] = new ObjModel("lock5", "truck.obj", 1, 1, 1);
            SetupLock(meslock[4]);

            groundPlane.Load("2.jpg");
            groundPlane.Position = new Vector3(0, -0.2f, 0);
            groundPlane.Rotation = new Vector3(90, 0, 0);
            world.Add(groundPlane);
            world.Add(car);
            Util.Set3DMode();
           // Fog.CreateFog(FogMode.Exp, 20, 200, 0.02f);
        }


        void SetupSprite(Mesh newsprite)
        {
            if (newsprite != null)
            {

                Vector3 pos = new Vector3(34 + (float)(random.NextDouble() * 100* random.NextDouble()), -0.2f , -34 + (float)(random.NextDouble() * 100 * random.NextDouble()));// поправить на корректное создание координат
                newsprite.Position = pos;
                this.world.Add(newsprite);
                Console.WriteLine(newsprite.Position.X+" "+ newsprite.Position+" "+ newsprite.Position);
            }
        }
        void SetupLock(Mesh newLock)
        {
            if (newLock != null)
            {

                Vector3 pos = new Vector3(55 + (float)(random.NextDouble() * 100 * random.NextDouble()), -0.2f, -18 + (float)(random.NextDouble() * 100 * random.NextDouble()));// поправить на корректное создание координат
                newLock.Position = pos;
                this.world.Add(newLock);
            }
        }


        #region OnUnload
        protected override void OnUnload(EventArgs e)
        {
            font.Dispose();
            Util.ClearArrays();
        }
        #endregion

        /// <summary>
        /// Called when your window is resized. Set your Frontport here. It is also
        /// a good place to set Up your projection Matrix (which probably changes
        /// along when the aspect ratio of your window).
        /// </summary>
        /// <param name="e">Contains information on the new Width and Size of the GameWindow.</param>
        protected override void OnResize(EventArgs e)
        {
            Util.Resize(Width, Height, 1, 1000);
        }

        /// <summary>
        /// Called when it is time to setup the next frame.
        /// </summary>
        /// <param name="e">Contains timing information for framerate independent logic.</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Keyboard[Key.Escape])
                Exit();

     
            float speed = (float)(5 * e.Time);

            if (Keyboard[Key.Left])
                if (carinfo.speed != 0)
                    if (carinfo.speed < 0)
                    {
                        car.Rotation.Y -= 20 * speed;
                        cam.Rotation.Y -= 20 * speed;
                    }
                    else
                    {
                        car.Rotation.Y += 20 * speed;
                        cam.Rotation.Y += 20 * speed;
                    }
            if (Keyboard[Key.Right])
                if (carinfo.speed != 0)
                    if (carinfo.speed < 0)
                    {
                        car.Rotation.Y += 20 * speed;
                        cam.Rotation.Y += 20 * speed;
                    }
                       
                    else
                    {
                        car.Rotation.Y -= 20 * speed;
                        cam.Rotation.Y -= 20 * speed;
                    }
                        

            if (Keyboard[Key.Up])
            {
                carinfo.speed += carinfo.up;

                if (carinfo.speed > carinfo.max) carinfo.speed = carinfo.max;
            }
            else
            {
                if (Keyboard[Key.Down])
                {
                    carinfo.speed -= carinfo.up;
                    if (carinfo.speed < -carinfo.max / 2) carinfo.speed = -carinfo.max / 2;
                }
                else
                {
                    if (carinfo.speed < 0) carinfo.speed += carinfo.down;
                    else carinfo.speed -= carinfo.down;

                    if (Math.Abs(carinfo.speed) <= 0.5f) carinfo.speed = 0;
                }
            }

            float spd = 1;
            if (Keyboard[Key.ShiftLeft]) spd = 2;
            if (Keyboard[Key.W]) cam.MoveXZ(spd, 0);
            if (Keyboard[Key.S]) cam.MoveXZ(-spd, 0);
            if (Keyboard[Key.A]) cam.MoveXZ(0, -spd);
            if (Keyboard[Key.D]) cam.MoveXZ(0, spd);
            if (Keyboard[Key.R]) cam.Position.Y++;
            if (Keyboard[Key.F]) if (cam.Position.Y > 1) cam.Position.Y--;
            if (Mouse[MouseButton.Left])
            {
                cam.TurnXZ(Mouse.X - _oldMouseX);
                cam.LookUpXZ(Mouse.Y - _oldMouseY);
            }
            _oldMouseX = Mouse.X; _oldMouseY = Mouse.Y;

            Vector3 oldPos = car.Position; 
            cam.Position = car.Position;
            cam.Position.Y = car.Position.Y+2;
            car.MoveXZ(speed * carinfo.speed);
            cam.MoveXZ(speed * carinfo.speed);
            cam.LookUpXZ(car.Position.Y - oldPos.Y);

            if (Intersection.CheckCollisionBB(ref world, oldPos, car.Position, ref car) == true)
            {
                var  rrr=Intersection.CheckBB1(ref world, oldPos, ref car, ref car);
                Log.WriteDebugLine(rrr.Name);
                if (rrr.Name.Contains("sprite"))
                {
                         world.Remove(rrr);
                         carinfo.getsprites = carinfo.getsprites+1;
                   

                }
                if (rrr.Name.Contains("lock"))
                {
                    DialogResult dialogResult = MessageBox.Show("Внимание! Произошло стокновение с грузовиком. Восстановить исходное положение вашего авто?", "Стокновение", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                      
                        Log.WriteDebugLine("pok!");
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        carinfo.speed = 0;
                        car.Position = oldPos;
                    }
                 
                }
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Settings.NumOfObjects = 0;
            GL.Clear(ClearBufferMask.DepthBufferBit);
            base.OnRenderFrame(e);
            cam.UpdateXZ();
            Frustum.CalculateFrustum();
            world.Render(); 
            Light.Disable();
            text.Begin();
            text.Print("Sprite get count: " + carinfo.getsprites, font, Color.White);
            text.End();
            Light.Enable();
            SwapBuffers();
        }

    }
}
