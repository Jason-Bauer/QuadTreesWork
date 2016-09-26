using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace QuadTreeStarter
{
    class QuadTreeNode
    {
        
        // The maximum number of objects in a quad
        // before a subdivision occurs
        private const int MAX_OBJECTS_BEFORE_SUBDIVIDE = 3;
       

        
        // The game objects held at this level of the tree
        private List<GameObject> _objects;

        // This quad's rectangle area
        private Rectangle _rect;

        // This quad's divisions
        private QuadTreeNode[] _divisions;
        

       
        /// <summary>
        /// The divisions of this quad
        /// </summary>
        public QuadTreeNode[] Divisions { get { return _divisions; } }

        /// <summary>
        /// This quad's rectangle
        /// </summary>
        public Rectangle Rectangle { get { return _rect; } }

        /// <summary>
        /// The game objects inside this quad
        /// </summary>
        public List<GameObject> GameObjects { get { return _objects; } }
      

        
        /// <summary>
        /// Creates a new Quad Tree
        /// </summary>
        /// <param name="x">This quad's x position</param>
        /// <param name="y">This quad's y position</param>
        /// <param name="width">This quad's width</param>
        /// <param name="height">This quad's height</param>
        public QuadTreeNode(int x, int y, int width, int height)
        {
            // Save the rectangle
            _rect = new Rectangle(x, y, width, height);

            // Create the object list
            _objects = new List<GameObject>();

            // No divisions yet
            _divisions = null;
        }
        

        /// <summary>
        /// Adds a game object to the quad.  If the quad has too many
        /// objects in it, and hasn't been divided already, it should
        /// be divided
        /// </summary>
        /// <param name="gameObj">The object to add</param>
        public void AddObject(GameObject gameObj)
        {//checks if the gameobj fits in this quad
          if(Rectangle.Contains(gameObj.Rectangle))
            {
                if (_divisions != null)
                {
                    foreach (QuadTreeNode element in _divisions)
                      
                    {
                        if (element.Rectangle.Contains(gameObj.Rectangle))
                        {
                            element.AddObject(gameObj);
                        }
                    }


                }
                else
                {
                    _objects.Add(gameObj);
                }
                if (_objects.Count > 4)
                {
                    Divide();
                }

            }
            else
            {

            }
            
            
            // ACTIVITY: Complete this method
        }

        /// <summary>
        /// Divides this quad into 4 smaller quads.  Moves any game objects
        /// that are completely contained within the new smaller quads into
        /// those quads and removes them from this one.
        /// </summary>
        public void Divide()
        {
            if (_divisions ==null)
            {

                _divisions = new QuadTreeNode[4];

                QuadTreeNode a = new QuadTreeNode(Rectangle.X, Rectangle.Y, Rectangle.Width / 2, Rectangle.Height / 2);
                QuadTreeNode b = new QuadTreeNode(Rectangle.X + (Rectangle.Width / 2), Rectangle.Y, Rectangle.Width / 2, Rectangle.Height / 2);
                QuadTreeNode c = new QuadTreeNode(Rectangle.X, Rectangle.Y + (Rectangle.Height / 2), Rectangle.Width / 2, Rectangle.Height / 2);
                QuadTreeNode d = new QuadTreeNode(Rectangle.X + (Rectangle.Width / 2), Rectangle.Y + (Rectangle.Height / 2), Rectangle.Width / 2, Rectangle.Height / 2);

                _divisions[0] = a;
                _divisions[1] = b;
                _divisions[2] = c;
                _divisions[3] = d;


                foreach (GameObject element in _objects)
                {
                    if (_divisions[0].Rectangle.Contains(element.Rectangle))
                    {
                        AddObject(element);
                    }
                    if (_divisions[1].Rectangle.Contains(element.Rectangle))
                    {
                        AddObject(element);
                    }
                    if (_divisions[2].Rectangle.Contains(element.Rectangle))
                    {
                        AddObject(element);
                    }
                    if (_divisions[3].Rectangle.Contains(element.Rectangle))
                    {
                        AddObject(element);
                    }
                }



            }
            else
            {

            }






            // ACTIVITY: Complete this method
        }

        /// <summary>
        /// Recursively populates a list with all of the rectangles in this
        /// quad and any subdivision quads.  Use the "AddRange" method of
        /// the list class to add the elements from one list to another.
        /// </summary>
        /// <returns>A list of rectangles</returns>
        public List<Rectangle> GetAllRectangles()
        {
            List<Rectangle> rects = new List<Rectangle>();

            // ACTIVITY: Complete this method
            rects.Add(Rectangle);
            if (_divisions != null)
            {
                foreach(QuadTreeNode element in _divisions)
                {
                    rects.AddRange(element.GetAllRectangles());
                }
            }



                return rects;
        }

        /// <summary>
        /// A possibly recursive method that returns the
        /// smallest quad that contains the specified rectangle
        /// </summary>
        /// <param name="rect">The rectangle to check</param>
        /// <returns>The smallest quad that contains the rectangle</returns>
        public QuadTreeNode GetContainingQuad(Rectangle rect)
        {
            // ACTIVITY: Complete this method

            if (Rectangle.Contains(rect))
            {
               
                if (_divisions != null)
                {
                    foreach (QuadTreeNode element in _divisions)
                    {
                        if (element.Rectangle.Contains(rect))
                        {
                            
                            return element.GetContainingQuad(rect);
                        }
                       
                    }
                    return this;
               }
                else
                {
                    return this;
                }
            }


            // Return null if this quad doesn't completely contain
            // the rectangle that was passed in
            return null;
        }
       
    }
}
