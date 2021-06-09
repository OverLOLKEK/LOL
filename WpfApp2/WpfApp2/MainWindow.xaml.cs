using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp2
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		DispatcherTimer gameTimer = new DispatcherTimer();
		bool goLeft, goRight, goDown, goUp;
		int speed = 8;
		Rect MarioHitBox;
		int EnemySpeed = 10;
		int EnemyMoveStep = 80;
		int currentEnemytStep;
		int score = 0;



		private void CanvasKeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Left)
			{

				goLeft = false;

			}

			if (e.Key == Key.Right)
			{

				goRight = false;

			}

			if (e.Key == Key.Space)
			{

				goUp = false;
				goDown = true;



			}
		}

		public MainWindow()
		{
			InitializeComponent();

			GameSetUp();
		}

		private void CanvasKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Left)
			{
				goLeft = true;
			}

			if (e.Key == Key.Right)
			{
				goRight = true;
			}

			if (e.Key == Key.Space)
			{
				goUp = true;
				goDown = false;
			}
		}


		private void GameSetUp()
		{

			MyCanvas.Focus();
			gameTimer.Tick += GameLoop;
			gameTimer.Interval = TimeSpan.FromMilliseconds(20);
			gameTimer.Start();
			currentEnemytStep = EnemyMoveStep;

			//ImageBrush pacmanImage = new ImageBrush();
			//MarioImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/+++.jpg"));
			//Mario.Fill = MarioImage;

			//ImageBrush Turtle = new ImageBrush();
			//Turtle.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/+++.jpg"));
			//TurtleShrek.Fill = Turtle;

			//ImageBrush Turtle1 = new ImageBrush();
			//Turtle1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/+++.jpg"));
			//TurtleShrek1.Fill = Turtle1;

			//ImageBrush Turtle2 = new ImageBrush();
			//Turtle2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/+++.jpg"));
			//TurtleShrek2.Fill = Turtle2;

		}

		private void GameLoop(object sender, EventArgs e)
		{


			if (goRight)
			{
				Canvas.SetLeft(Mario, Canvas.GetLeft(Mario) + speed);
				// pacman.Right -= speed;
			}
			if (goLeft)
			{
				Canvas.SetLeft(Mario, Canvas.GetLeft(Mario) - speed);
			}
			if (goUp)
			{
				Canvas.SetTop(Mario, Canvas.GetTop(Mario) - speed);
			}
			if (goDown)
			{
				Canvas.SetTop(Mario, Canvas.GetTop(Mario) + speed);
			}



			if (goUp && Canvas.GetTop(Mario) < 1)
			{

				goUp = false;
			}
			if (goLeft && Canvas.GetLeft(Mario) - 10 < 1)
			{

				goLeft = false;
			}
			if (goRight && Canvas.GetLeft(Mario) + 70 > Application.Current.MainWindow.Width)
			{

				goRight = false;
			}


			MarioHitBox = new Rect(Canvas.GetLeft(Mario), Canvas.GetTop(Mario), Mario.Width, Mario.Height);


			foreach (var x in MyCanvas.Children.OfType<Rectangle>())
			{
				Rect hitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);



				if ((string)x.Tag == "Groun")
				{
					if (goLeft == true && MarioHitBox.IntersectsWith(hitBox))
					{
						Canvas.SetLeft(Mario, Canvas.GetLeft(Mario) + 5);

						goLeft = false;
					}
					if (goRight == true && MarioHitBox.IntersectsWith(hitBox))
					{
						Canvas.SetLeft(Mario, Canvas.GetLeft(Mario) - 5);

						goRight = false;
					}
					if (goDown == true && MarioHitBox.IntersectsWith(hitBox))
					{
						Canvas.SetTop(Mario, Canvas.GetTop(Mario) - 5);

						goDown = false;
					}
					if (goUp == true && MarioHitBox.IntersectsWith(hitBox))
					{
						Canvas.SetTop(Mario, Canvas.GetTop(Mario) + 5);

						goUp = false;
					}
				}


				if ((string)x.Tag == "Coin")
				{
					if (MarioHitBox.IntersectsWith(hitBox) && x.Visibility == Visibility.Visible)
					{
						x.Visibility = Visibility.Hidden;
						score++;
					}
				}


				if ((string)x.Tag == "Enemy")
				{
					if (MarioHitBox.IntersectsWith(hitBox))
					{

					}


					if (x.Name.ToString() == "TurtleShrek2")
					{
						Canvas.SetLeft(x, Canvas.GetLeft(x) - EnemySpeed);
					}
					else
					{
						Canvas.SetLeft(x, Canvas.GetLeft(x) + EnemySpeed);
					}

					currentEnemytStep--;

					if (currentEnemytStep < 1)
					{
						currentEnemytStep = EnemyMoveStep;
						EnemySpeed = -EnemySpeed;
					}
				}
			}







		}

		private void CameraMove()
		{
			void Update()
			{
				// transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
			}
		}

		private void GameOver(string message)
		{
			gameTimer.Stop();
			MessageBox.Show(message, "Game Over Lox");

			System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
			Application.Current.Shutdown();
		}




	}
}
