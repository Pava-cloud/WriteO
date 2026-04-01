using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteO
{
    internal interface IMode
    {
        void Show();
        void Draw();
        void HandleSelection();
    }
}
