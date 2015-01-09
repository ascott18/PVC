﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
	interface IInteractible
	{
		void Draw(Graphics graphics);

		void Interact();
	}
}