using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Steering_Text
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Vector2 displaySize;
		Vector2 screenSize;

		Texture2D image, pixel;
		List<Node> points;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			displaySize = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
			screenSize = new Vector2(displaySize.X, (int)displaySize.X / 16 * 9);

			IsMouseVisible = true;
			Window.AllowUserResizing = false;
			Window.IsBorderless = false;
			Window.Position = new Point((int)(displaySize.X / 2 - screenSize.X / 2), (int)(displaySize.Y / 2 - screenSize.Y / 2));
			Window.Title = "";

			graphics.IsFullScreen = false;
			graphics.PreferredBackBufferWidth = (int)screenSize.X;
			graphics.PreferredBackBufferHeight = (int)screenSize.Y;
			graphics.ApplyChanges();
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		private List<Node> getPoints(Texture2D image)
		{
			List<Node> points = new List<Node>();
			Color[,] data = new Color[image.Width, image.Height];
			Color[] rawData = new Color[image.Width * image.Height];
			image.GetData(rawData);

			for (int i = 0; i < data.Length; i++)
			{
				int x = i % image.Width;
				int y = i / image.Width;
				data[x, y] = rawData[i];
			}

			for (int y = 0; y < image.Height; y++)
			{
				for (int x = 0; x < image.Width; x++)
				{
					try
					{
						//if (data[x, y].A != 0 && data[x - 1, y].A == 0) { points.Add(new Node(new Vector2(x, y))); }
						//else if (data[x, y].A != 0 && data[x + 1, y].A == 0) { points.Add(new Node(new Vector2(x, y))); }
						//else if (data[x, y].A != 0 && data[x, y - 1].A == 0) { points.Add(new Node(new Vector2(x, y))); }
						//else if (data[x, y].A != 0 && data[x, y + 1].A == 0) { points.Add(new Node(new Vector2(x, y))); }

						if (data[x, y].R < 100 && data[x, y].G < 100 && data[x, y].B < 100 && Util.GetRandom(0, 4) == 0) { points.Add(new Node(new Vector2(x, y))); }
					}
					catch { }
				}
			}
			return points;
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			image = Content.Load<Texture2D>("preschen");
			pixel = Content.Load<Texture2D>("pixel");
			points = getPoints(image);
		}

		protected override void UnloadContent()
		{

		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			foreach (Node point in points)
			{
				point.update();
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.White);

			spriteBatch.Begin();

			foreach (Node point in points)
			{
				spriteBatch.Draw(pixel, new Vector2(point.pos.X, point.pos.Y), Color.White);
			}
			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
