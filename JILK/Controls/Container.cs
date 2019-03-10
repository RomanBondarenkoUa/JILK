using System;
using System.Collections.Generic;
using System.Text;
using JILK.Interfaces;

namespace JILK.Controls
{
    // TODO: class that can be resized and that provides sticking to container sides
    class Container : IGUIcontainer
    {
        private int height;
        private int width;
        private bool isStateChanged = true;

        public int Height
        {
            get { return height; }
            set
            {
                width = value;
                throw new NotImplementedException();
            }
        }
        public int Width
        {
            get { return width; }
            set
            {
                height = value;
                throw new NotImplementedException();

            }
        }
        public List<IGUIelement> Chilren { get; private set; }

        public Container(int height, int width)
        {
            throw new NotImplementedException();
        }
    }
}
