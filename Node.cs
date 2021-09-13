using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steering_Text
{
	class Node
	{
		private float maxSpeed = 10;
		private float maxForce = 1;
		public Vector2 pos = new Vector2(Util.GetRandom(0, 200), Util.GetRandom(0, 200));
		private Vector2 vel = new Vector2(Util.GetRandom(0, 1), Util.GetRandom(0, 1));
		private Vector2 acc = new Vector2();
		private Vector2 target;

		public Node(Vector2 target)
		{
			this.target = target;
		}

		private Vector2 arrive(Vector2 target)
		{
			Vector2 desired = target - pos;
			float d = desired.Length();
			float speed = maxSpeed;
			if (d < 200) { speed = Util.map(0, 100, 0, maxSpeed, d); }
			desired.Normalize();
			desired *= speed;

			Vector2 steer = desired - vel;
			if (steer.Length() > maxForce) { steer.Normalize(); steer *= maxForce; }
			return steer;
		}

		private Vector2 flee(Vector2 target)
		{
			Vector2 desired = target - pos;
			float d = desired.Length();
			if (d < 100)
			{
				desired.Normalize();
				desired *= -1;
				Vector2 st = desired - vel;
				if (st.Length() > maxForce) { st.Normalize(); st *= maxForce; }
				return st;
			}
			else { return new Vector2(0, 0); }
		}

		public void update()
		{
			Vector2 arrive = this.arrive(target);
			Vector2 mouse = Mouse.GetState().Position.ToVector2();
			Vector2 flee = this.flee(mouse);

			arrive *= 1;
			flee *= 5;

			acc += arrive + flee;

			pos += vel;
			vel += acc;
			acc *= 0;
		}		
	}
}
