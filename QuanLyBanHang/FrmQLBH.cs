using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace QuanLyBanHang
{
    public partial class FrmQLBH : Form
    {
        bool isAddItem = false;
        public FrmQLBH()
        {
            InitializeComponent();
        }
        SqlConnection con;
        private void FrmQLBH_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=QLBanHang;Persist Security Info=True;User ID=sa;Password=Abc@123");
            con.Open();
            HienThi();
            gbChiTiet.Enabled = false;
            btLuu.Enabled = false;
            btHuy.Enabled = false;
            btThem.Enabled = true;
            btSua.Enabled = true;
            btThoat.Enabled = true;
            txtTimMDH.Focus();
        }
        public void HienThi()
        {
            string sqlSelect = "select * from KhachHang";
            SqlCommand cmd = new SqlCommand(sqlSelect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dvBH.DataSource = dt;

        }

        private void FrmQLBH_FormClosing(object sender, FormClosingEventArgs e)
        {
            con.Close();
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            isAddItem = true;
            gbChiTiet.Enabled = true;
            btSua.Enabled = false;
            btThoat.Enabled = false;
            btHuy.Enabled = true;
            btLuu.Enabled = true;
            DateTime now = DateTime.Now;
            txtHD.Text = now.ToString("yyMMMddhhmm");
            txtMDH.Text = "";
            txtTenSP.Text = "";
            txtTenKH.Text = "";
            txtDC.Text = "";
            dtMua.Text = "";
            txtFish.Text = "";
            txtMua.Text = "";
            txtLN.Text = "";
            cbShip.Text = "";
            dtShip.Text = "";
            txtLink.Text = "";
            cbTrack.Text = "";
            txtEmail.Text = "";
            txtNote.Text = "";
            txtMDH.Focus();
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            gbChiTiet.Enabled = true;
            btThem.Enabled = false;
            btLuu.Enabled = true;
            btHuy.Enabled = true;
            btThoat.Enabled = false;
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Thoát Chương Trình ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
== DialogResult.Yes)
                Application.Exit();
            con.Close();
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            txtHD.Text = "";
            txtMDH.Text = "";
            txtTenSP.Text = "";
            txtTenKH.Text = "";
            txtDC.Text = "";
            dtMua.Text = "";
            txtFish.Text = "";
            txtMua.Text = "";
            txtLN.Text = "";
            cbShip.Text = "";
            dtShip.Text = "";
            txtLink.Text = "";
            cbTrack.Text = "";
            txtEmail.Text = "";
            txtNote.Text = "";
            btThem.Enabled = true;
            btSua.Enabled = true;
            btHuy.Enabled = false;
            btLuu.Enabled = false;
            btThoat.Enabled = true;
            gbChiTiet.Enabled = false;
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            string MDH = "", TenKH = "", TenSP = "", ShipChua = "";
            MDH = txtMDH.Text;
            TenKH = txtTenKH.Text;
            TenSP = txtTenSP.Text;
            ShipChua = cbShip.Text;
            if (MDH.Length == 0)
            {
                MessageBox.Show("Cần Nhập Mã Đơn Hàng FishPond !");
                txtMDH.Focus();
                return;
            }
            if (TenKH.Length == 0)
            {
                MessageBox.Show("Cần Thêm Tên Khách Hàng !");
                txtTenKH.Focus();
                return;
            }
            if (TenSP.Length == 0)
            {
                MessageBox.Show("Cần Thêm Tên Sản Phẩm !");
                txtTenSP.Focus();
                return;
            }
            if (ShipChua.Length == 0)
            {
                MessageBox.Show("Món Hàng Đã Ship Chưa ?");
                cbShip.Focus();
                return;
            }
            if (isAddItem == true)

                try
                {
                    string sqlLuu = "insert into KhachHang values (@HoaDon,@MaDonHang,@TenSP,@TenKH,@DiaChi,@NgayMua,@GiaFish,@GiaMua,@LoiNhuan,@ShipChua,@NgayShip,@LinkMua,@Tracking,@Email,@GhiChu)";
                    SqlCommand cmd = new SqlCommand(sqlLuu, con);
                    cmd.Parameters.AddWithValue("HoaDon", txtHD.Text);
                    cmd.Parameters.AddWithValue("MaDonHang", txtMDH.Text);
                    cmd.Parameters.AddWithValue("TenSP", txtTenSP.Text);
                    cmd.Parameters.AddWithValue("TenKH", txtTenKH.Text);
                    cmd.Parameters.AddWithValue("DiaChi", txtDC.Text);
                    cmd.Parameters.AddWithValue("NgayMua", dtMua.Value.ToString());
                    cmd.Parameters.AddWithValue("GiaFish", txtFish.Text);
                    cmd.Parameters.AddWithValue("GiaMua", txtMua.Text);
                    cmd.Parameters.AddWithValue("LoiNhuan", txtLN.Text);
                 //   cmd.Parameters.AddWithValue("LoiNhuan", txtLN.ToString());
                    cmd.Parameters.AddWithValue("ShipChua", cbShip.Text);
                    cmd.Parameters.AddWithValue("NgayShip", dtShip.Value.ToString());
                    cmd.Parameters.AddWithValue("LinkMua", txtLink.Text);
                    cmd.Parameters.AddWithValue("Tracking", cbTrack.Text);
                    cmd.Parameters.AddWithValue("Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("GhiChu", txtNote.Text);
                    cmd.ExecuteNonQuery();
                    HienThi();
                    MessageBox.Show("Lưu Sản Phẩm Thành Công !","Thông Báo ");
                    gbChiTiet.Enabled = false;
                    btThem.Enabled = true;
                    btSua.Enabled = true;
                    btHuy.Enabled = false;
                    btLuu.Enabled = false;
                    btThoat.Enabled = true;
                    txtHD.Text = "";
                    txtMDH.Text = "";
                    txtTenSP.Text = "";
                    txtTenKH.Text = "";
                    txtDC.Text = "";
                    dtMua.Text = "";
                    txtFish.Text = "";
                    txtMua.Text = "";
                    txtLN.Text = "";
                    cbShip.Text = "";
                    dtShip.Text = "";
                    txtLink.Text = "";
                    cbTrack.Text = "";
                    txtEmail.Text = "";
                    txtNote.Text = "";
                    isAddItem = false;
                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            else
            {
                try
                {
                    string sqlSua = "update KhachHang set MaDonHang = @MaDonHang,TenSP = @TenSP,TenKH = @TenKH,DiaChi = @DiaChi,NgayMua = @NgayMua,GiaFish = @GiaFish, GiaMua = @GiaMua,LoiNhuan = @LoiNhuan,ShipChua = @ShipChua,NgayShip = @NgayShip,LinkMua= @LinkMua,Tracking = @Tracking,Email = @Email,GhiChu = @GhiChu where HoaDon = @HoaDon";
                    SqlCommand cmd = new SqlCommand(sqlSua, con);
                    cmd.Parameters.AddWithValue("HoaDon", txtHD.Text);
                    cmd.Parameters.AddWithValue("MaDonHang", txtMDH.Text);
                    cmd.Parameters.AddWithValue("TenSP", txtTenSP.Text);
                    cmd.Parameters.AddWithValue("TenKH", txtTenKH.Text);
                    cmd.Parameters.AddWithValue("DiaChi", txtDC.Text);
                    cmd.Parameters.AddWithValue("NgayMua", dtMua.Value.ToString());
                    cmd.Parameters.AddWithValue("GiaFish", txtFish.Text);
                    cmd.Parameters.AddWithValue("GiaMua", txtMua.Text);
                    cmd.Parameters.AddWithValue("LoiNhuan", txtLN.Text);
                    cmd.Parameters.AddWithValue("ShipChua", cbShip.Text);
                    cmd.Parameters.AddWithValue("NgayShip", dtShip.Value.ToString());
                    cmd.Parameters.AddWithValue("LinkMua", txtLink.Text);
                    cmd.Parameters.AddWithValue("Tracking", cbTrack.Text);
                    cmd.Parameters.AddWithValue("Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("GhiChu", txtNote.Text);
                    cmd.ExecuteNonQuery();
                    HienThi();
                    MessageBox.Show("Sửa Sản Phẩm Thành Công !", "Thông Báo ");
                    gbChiTiet.Enabled = false;
                    btThem.Enabled = true;
                    btSua.Enabled = true;
                    btHuy.Enabled = false;
                    btLuu.Enabled = false;
                    btThoat.Enabled = true;
                    txtHD.Text = "";
                    txtMDH.Text = "";
                    txtTenSP.Text = "";
                    txtTenKH.Text = "";
                    txtDC.Text = "";
                    dtMua.Text = "";
                    txtFish.Text = "";
                    txtMua.Text = "";
                    txtLN.Text = "";
                    cbShip.Text = "";
                    dtShip.Text = "";
                    txtLink.Text = "";
                    cbTrack.Text = "";
                    txtEmail.Text = "";
                    txtNote.Text = "";
                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dvBH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dvBH.CurrentRow.Index;
            txtHD.Text = dvBH.Rows[i].Cells[0].Value.ToString();
            txtMDH.Text = dvBH.Rows[i].Cells[1].Value.ToString();
            txtTenSP.Text = dvBH.Rows[i].Cells[2].Value.ToString();
            txtTenKH.Text = dvBH.Rows[i].Cells[3].Value.ToString();
            txtDC.Text = dvBH.Rows[i].Cells[4].Value.ToString();    
            dtMua.Text = dvBH.Rows[i].Cells[5].Value.ToString();
            txtFish.Text = dvBH.Rows[i].Cells[6].Value.ToString();
            txtMua.Text = dvBH.Rows[i].Cells[7].Value.ToString();
            txtLN.Text = dvBH.Rows[i].Cells[8].Value.ToString();
            cbShip.Text = dvBH.Rows[i].Cells[9].Value.ToString();
            dtShip.Text = dvBH.Rows[i].Cells[10].Value.ToString();
            txtLink.Text = dvBH.Rows[i].Cells[11].Value.ToString();
            cbTrack.Text = dvBH.Rows[i].Cells[12].Value.ToString();
            txtEmail.Text = dvBH.Rows[i].Cells[13].Value.ToString();
            txtNote.Text = dvBH.Rows[i].Cells[14].Value.ToString();
        }

        private void txtTimMDH_TextChanged(object sender, EventArgs e)
        {
            if (txtTimMDH.Text != string.Empty)
                try
                {
                    string sqlTimKiem = "select * from KhachHang where MaDonHang = @MaDonHang";
                    SqlCommand cmd = new SqlCommand(sqlTimKiem, con);
                    cmd.Parameters.AddWithValue("MaDonHang", txtTimMDH.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dvBH.DataSource = dt;

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            else
            {
                HienThi();
            }
        }

        private void txtTimTenKH_TextChanged(object sender, EventArgs e)
        {
            if (txtTimTenKH.Text != string.Empty)
                try
                {
                    string sqlTimKiem = "select * from KhachHang where TenKH = @TenKH";
                    SqlCommand cmd = new SqlCommand(sqlTimKiem, con);
                    cmd.Parameters.AddWithValue("TenKH", txtTimTenKH.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dvBH.DataSource = dt;

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            else
            {
                HienThi();
            }
        }

        private void cbTimTheoDK_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTimDK.Text == "Ship Later")
                try
                {
                    string sqlTimKiem = "select * from KhachHang where ShipChua = @ShipChua";
                    SqlCommand cmd = new SqlCommand(sqlTimKiem, con);
                    cmd.Parameters.AddWithValue("ShipChua", cbTimDK.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dvBH.DataSource = dt;

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            else if (cbTimDK.Text == "Shipped")
                try
                {
                    string sqlTimKiem = "select * from KhachHang where ShipChua = @ShipChua";
                    SqlCommand cmd = new SqlCommand(sqlTimKiem, con);
                    cmd.Parameters.AddWithValue("ShipChua", cbTimDK.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dvBH.DataSource = dt;

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            else if (cbTimDK.Text == "Refund")
                try
                {
                    string sqlTimKiem = "select * from KhachHang where ShipChua = @ShipChua";
                    SqlCommand cmd = new SqlCommand(sqlTimKiem, con);
                    cmd.Parameters.AddWithValue("ShipChua", cbTimDK.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dvBH.DataSource = dt;

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
        }

        private void cbTimTrack_SelectedIndexChanged(object sender, EventArgs e)
        {
              if (cbTimTrack.Text == "Chưa Track")
                try
                {
                    string sqlTimKiem = "select * from KhachHang where Tracking = @Tracking";
                    SqlCommand cmd = new SqlCommand(sqlTimKiem, con);
                    cmd.Parameters.AddWithValue("Tracking", cbTimTrack.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dvBH.DataSource = dt;

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            else           

            if (cbTimTrack.Text == "Có Track")
                try
                {
                    string sqlTimKiem = "select * from KhachHang where Tracking = @Tracking";
                    SqlCommand cmd = new SqlCommand(sqlTimKiem, con);
                    cmd.Parameters.AddWithValue("Tracking", cbTimTrack.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dvBH.DataSource = dt;

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
               else
            if (cbTimTrack.Text == "Refund")
                try
                {
                    string sqlTimKiem = "select * from KhachHang where Tracking = @Tracking";
                    SqlCommand cmd = new SqlCommand(sqlTimKiem, con);
                    cmd.Parameters.AddWithValue("Tracking", cbTimTrack.Text);
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dvBH.DataSource = dt;

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }        
        }

        private void cbShip_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbShip.Text == "Shipped")
                {
                    double fish, mua, kq,q1;
                    fish = double.Parse(txtFish.Text);
                    mua = double.Parse(txtMua.Text);
                    kq = (fish * 0.95) - mua;
                    q1 = (kq * 17000) - (mua * 1000);
                    txtLN.Text = q1.ToString("N");                    
                }
                if (cbShip.Text == "Ship Later")
                {
                    txtLN.Text = "";
                }
                if (cbShip.Text == "Refund")
                {
                    txtLN.Text = "";
                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void dvBH_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
           for (int i = 0; i <dvBH.Rows.Count; i++)
            {
                DataGridViewCellStyle shipped = dvBH.Rows[i].DefaultCellStyle;
                shipped.BackColor = Color.LightGray;
            }    
        }

        private void btTinh_Click(object sender, EventArgs e)
        {
            lbThanhTien.Text = "0";
            for (int i = 0; i < dvBH.Rows.Count; i++)
            {
                lbThanhTien.Text = Convert.ToString(double.Parse(lbThanhTien.Text)+ double.Parse(dvBH.Rows[i].Cells[8].Value.ToString()));

            }   
         }

        private void btXoa_Click(object sender, EventArgs e)
        {
            lbThanhTien.Text = "";
        }
    }
}
