using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace CSharpVAST
{
    class PanelRender
    {
        #region 移動方向之類型
        const int LEFT_DOWN = 1;
        const int DOWN = 2;
        const int RIGHT_DOWN = 3;
        const int LEFT = 4;
        const int RIGHT = 6;
        const int LEFT_UP = 7;
        const int UP = 8;
        const int RIGHT_UP = 9;
        #endregion

        Panel myPanel;

        public PanelRender(Panel panel)  // 初始化設定
        {
            myPanel = panel;
        }

        public void Show(Point aoi_center, UInt16 AOI_RADIUS, List<Node> neighborList, String SelfName, int SelfDirect, ulong listener)  // 顯示在營幕上
        {
            Pen myPen = new Pen(Color.Black, 1);
            Size center_size = new Size(15, 15);  // 節點大小
            Rectangle center_rect = new Rectangle(aoi_center.X - (center_size.Height / 2), aoi_center.Y - (center_size.Width / 2), center_size.Height, center_size.Width);
            Size aoi_size = new Size(AOI_RADIUS * 2, AOI_RADIUS * 2);  // AOI 範圍大小
            Rectangle aoi_rect = new Rectangle(aoi_center.X - AOI_RADIUS, aoi_center.Y - AOI_RADIUS, aoi_size.Height, aoi_size.Width);

            myPanel.CreateGraphics().FillEllipse(Brushes.Gray, center_rect);  // 本身塗灰
            myPanel.CreateGraphics().DrawEllipse(myPen, center_rect);  // 本身節點之位置
            myPanel.CreateGraphics().DrawEllipse(myPen, aoi_rect);  // 本身節點之aoi範圍
            OrienRender(aoi_center.X, aoi_center.Y, SelfDirect, true);  // 畫移動方向

            myPanel.CreateGraphics().DrawString(SelfName, new Font("新細明體", 10), new SolidBrush(Color.Black), aoi_center);  // 本身節點的名字

            // 把鄰居的節點畫在 Panel 上面
            for (int i = 0; i < neighborList.Count; i++)
            {
                RectangleF center_neighbor = new RectangleF(neighborList[i].x - (center_size.Height / 2), neighborList[i].y - (center_size.Width / 2), center_size.Height, center_size.Width);
                PointF pos = new PointF(neighborList[i].x, neighborList[i].y);  // 鄰居的位置座標
                myPanel.CreateGraphics().DrawEllipse(myPen, center_neighbor);
                myPanel.CreateGraphics().DrawString(neighborList[i].name, new Font("新細明體", 10), new SolidBrush(Color.Black), pos);  // 鄰居節點的名字
                OrienRender(neighborList[i].x, neighborList[i].y, neighborList[i].direct, false);
            }

            ListenerRender(neighborList, aoi_center, listener);  // listener 是誰的虛線

            myPen.Dispose();
        }

        public void OrienRender(float x, float y, int dir, bool self)  // 畫每個節點的移動方向   parameter x : 節點 x 座標  , parameter y : 節點 y 座標  , parameter dir : 節點移動方向
        {
            Pen ancPen;  // 用來畫面對方向
            int side_1 = 4;  // 使用在 對角線的長度
            float side_2 = 5.6f;  // 使用在 直線的長度

            if (self)  // 若是自己的話使用黃色箭頭
                ancPen = new Pen(Color.Yellow, 1);
            else  // 若是鄰居的話使用紅色箭頭
                ancPen = new Pen(Color.Tomato, 1);
            ancPen.EndCap = LineCap.ArrowAnchor;  // 箭頭形狀線條末端點

            switch (dir)
            {
                case LEFT_DOWN:     // 左下
                    myPanel.CreateGraphics().DrawLine(ancPen, x + side_1, y - side_1, x - side_1, y + side_1);
                    break;
                case DOWN:          // 下
                    myPanel.CreateGraphics().DrawLine(ancPen, (float)x, (float)y - side_2, (float)x, (float)y + side_2);
                    break;
                case RIGHT_DOWN:    // 右下
                    myPanel.CreateGraphics().DrawLine(ancPen, x - side_1, y - side_1, x + side_1, y + side_1);
                    break;
                case LEFT:          // 左
                    myPanel.CreateGraphics().DrawLine(ancPen, (float)x + side_2, (float)y, (float)x - side_2, (float)y);
                    break;
                case RIGHT:         // 右
                    myPanel.CreateGraphics().DrawLine(ancPen, (float)x - side_2, (float)y, (float)x + side_2, (float)y);
                    break;
                case LEFT_UP:       // 左上
                    myPanel.CreateGraphics().DrawLine(ancPen, x + side_1, y + side_1, x - side_1, y - side_1);
                    break;
                case UP:            // 上
                    myPanel.CreateGraphics().DrawLine(ancPen, (float)x, (float)y + side_2, (float)x, (float)y - side_2);
                    break;
                case RIGHT_UP:      // 右上
                    myPanel.CreateGraphics().DrawLine(ancPen, x - side_1, y + side_1, x + side_1, y - side_1);
                    break;
            }
            ancPen.Dispose();
        }

        public void ListenerRender(List<Node> neighborList, Point center, ulong listener)  // 把 Listener 的線畫出來
        {
            Pen myPen = new Pen(Color.ForestGreen, 1);

            myPen.DashStyle = DashStyle.Dot;  // 點線
            myPen.EndCap = LineCap.DiamondAnchor;

            foreach (Node n in neighborList)
                if (n.id == listener)
                    myPanel.CreateGraphics().DrawLine(myPen, center.X, center.Y, n.x, n.y);

            myPen.Dispose();
        }
    }
}
