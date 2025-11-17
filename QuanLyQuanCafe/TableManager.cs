using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class TableManager : Form
    {
        
        public TableManager()
        {
            InitializeComponent();            
            loadBanAn();
            loadDanhMuc();
            loadComboboxTable(cbSwitchTable);
        }
        void loadDanhMuc()
        {
            List<DanhMuc> dsDanhMuc = DanhMuc_DAO.Instance.getDsDanhMuc();
            cbDanhMuc.DataSource = dsDanhMuc;
            cbDanhMuc.DisplayMember = "Name";
        }
        void loadDsFoodByIdDanhMuc(int id)
        {
            List<Food> dsFood = Food_DAO.Instance.getFoodByCategoryId(id);
            cbTen.DataSource = dsFood;
            cbTen.DisplayMember = "Name";
        }
        void loadBanAn()
        {
            flpTable.Controls.Clear();
            List<BanAn> dsBan = Manager_DAO.Instance.LoadTableList();
            foreach (BanAn dto in dsBan)
            {
                Button btn = new Button() { Width = Manager_DAO.TableWidth, Height = Manager_DAO.TableHeight };
                btn.Text = dto.Ten + Environment.NewLine + dto.TrangThai;
                btn.Click += btn_Click;
                btn.Tag = dto;

                switch (dto.TrangThai)
                {
                    case "Bàn đang trống":
                        btn.BackColor = Color.Goldenrod;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }
        void showBill(int id)
        {
            lsViewBill.Items.Clear();
            List<ThucDon> listBillIn4 = ThucDon_DAO.Instance.getListMenuByTable(id);
            float thanhTien = 0;
            foreach (ThucDon in4 in listBillIn4)
            {
                ListViewItem item = new ListViewItem(in4.TenMon.ToString());
                item.SubItems.Add(in4.SoLuong.ToString());
                item.SubItems.Add(in4.DonGia.ToString());
                item.SubItems.Add(in4.ThanhTien.ToString());
                thanhTien += in4.ThanhTien;
                lsViewBill.Items.Add(item);
            }
            CultureInfo c = new CultureInfo("vi-VN");
            // Thread.CurrentThread.CurrentCulture = c;

            txTongTien.Text = thanhTien.ToString("c", c);
        }
        void loadComboboxTable (ComboBox cb)
        {
            cb.DataSource = Manager_DAO.Instance.LoadTableList();
            cb.DisplayMember = "Ten";
        }
        void btn_Click(object sender, EventArgs e)
        {
            int idBan = ((sender as Button).Tag as BanAn).Id;
            lsViewBill.Tag = (sender as Button).Tag;
            showBill(idBan);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null) return;
            DanhMuc chon = cb.SelectedItem as DanhMuc;
            id = chon.Id;
            loadDsFoodByIdDanhMuc(id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            BanAn ba = lsViewBill.Tag as BanAn;
            if (ba == null)
            {
                MessageBox.Show("Hãy chọn bàn !");
                return;
            }
            int idBill = Bill_DAO.Instance.getUnCheckBillIdByTableId(ba.Id);
            int idFood = (cbTen.SelectedItem as Food).Id;
            int soLuong = (int)nmSoLuong.Value;
            if (idBill == -1)
            {
                Bill_DAO.Instance.insertBill(ba.Id);
                ThongTinBill_DAO.Instance.insertBillInfo(Bill_DAO.Instance.getMaxIdBill(), idFood, soLuong);
            }
            else
            {
                ThongTinBill_DAO.Instance.insertBillInfo(idBill, idFood, soLuong);
            }
            showBill(ba.Id);
            loadBanAn();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            BanAn ba = lsViewBill.Tag as BanAn;
            int idBill = Bill_DAO.Instance.getUnCheckBillIdByTableId(ba.Id);
            //int giamGia = (int)nmDiscount.Value;
            double tongTien = double.Parse(txTongTien.Text, System.Globalization.NumberStyles.Any, new System.Globalization.CultureInfo("vi-VN"));

            double final = tongTien; //= tongTien - (tongTien / 100) * giamGia;

            // Check for applied promotion
            string name = txGiamGia.Text.Trim();
            Discount promo = null;
            if (!string.IsNullOrEmpty(name))
            {
                promo = Discount_DAO.Instance.getDiscountByNameCode(name);
            }

            // Kiểm tra nếu promo không null trước khi sử dụng
            if (promo != null)
            {

                final -= (final / 100) * promo.GiaTri; // Assuming promo.Value is a percentage discount
            }

            // Nếu không có giảm giá nào được áp dụng, giá cuối cùng phải bằng giá gốc
            if (promo == null)
            {
                final = tongTien;
            }

            if (idBill != -1)
            {
                CultureInfo c = new CultureInfo("vi-VN");
                txTongTien.Text = final.ToString("c", c);
                if (MessageBox.Show(string.Format("Thanh toán hóa đơn cho {0}\n Tổng tiền - Tổng tiền / 100 x Giảm giá = {1} - {1} / 100 * {2} = {3}", ba.Ten, tongTien, (promo != null ? promo.GiaTri.ToString() : "0"), final), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    int giamGia = (promo != null  ? (int)promo.GiaTri : 0); // Sử dụng giá trị giảm giá hoặc 0 nếu promo là null
                    Bill_DAO.Instance.checkOut(idBill, giamGia, (float)final);
                    showBill(ba.Id);
                    loadBanAn();
                }
            }
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int id1 = (lsViewBill.Tag as BanAn).Id;
            int id2 = (cbSwitchTable.SelectedItem as BanAn).Id;
            if (MessageBox.Show(string.Format("Bạn có muốn chuyển bàn {0} qua bàn {1}", (lsViewBill.Tag as BanAn).Ten, (cbSwitchTable.SelectedItem as BanAn).Ten), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                Manager_DAO.Instance.SwitchTable(id1, id2);
                loadBanAn();
            }
        }

        private void btnApDung_Click(object sender, EventArgs e)
        {
            string name = txGiamGia.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập mã giảm giá (nếu có) !");
                return;
            }

            Discount promo = Discount_DAO.Instance.getDiscountByNameCode(name);
            if (promo == null)
            {
                MessageBox.Show("Mã khuyến mãi không hợp lệ hoặc đã hết hạn.");
                return;
            }

            BanAn ba = lsViewBill.Tag as BanAn;
            if (ba == null)
            {
                MessageBox.Show("Vui lòng chọn bàn !");
                return;
            }

            int idBill = Bill_DAO.Instance.getUnCheckBillIdByTableId(ba.Id);
            if (idBill == -1)
            {
                MessageBox.Show("Không tìm thấy hóa đơn cho bàn đã chọn.");
                return;
            }

            ApplyPromotion(promo, idBill, ba);
        }
        void ApplyPromotion(Discount promo, int idBill, BanAn ba)
        {
            
            showBill(ba.Id);
            loadBanAn();
        }
    }
}
