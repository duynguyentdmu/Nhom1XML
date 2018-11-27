using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace nhom1DemoXML
{
    public partial class ThongTinSinhVien : Form
    {
        private DataSet dataSet;
        private bool editInterface = false;
        private bool EditInterface
        {
            get { return editInterface; }
            set
            {
                gridviewSV.ReadOnly = !value;
                btnThem.Enabled = !value;
                btnSua.Text = value ? "LƯU" : "SỬA";
                btnXoa.Enabled = !value;
                btnTim.Enabled = !value;
                btnWrite.Enabled = !value;
                btnRead.Enabled = !value;
                tbMa.Enabled = !value;
                tbTen.Enabled = !value;
                tbLop.Enabled = !value;
                tbNoisinh.Enabled = !value;
                tbTim.Enabled = !value;
            }
        }
        private bool EmptyTextBox
        {
            get
            {
                if (tbMa.Text.Length > 0 || tbTen.Text.Length > 0 || tbLop.Text.Length > 0 ||
                    tbNoisinh.Text.Length > 0) return false;
                else return true;
            }
            set
            {
                if (value)
                {
                    tbMa.Text = "";
                    tbTen.Text = "";
                    tbLop.Text = "";
                    tbNoisinh.Text = "";
                }
            }
        }
        public ThongTinSinhVien()
        {
            InitializeComponent();
            dataSet = new DataSet();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        // nut thoat
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            EditInterface = false;
            btnSua.Enabled = true;
            try
            {
                if (dataSet.Tables.Count > 0) dataSet = new DataSet();
                dataSet.ReadXml("../../SinhVien.xml", XmlReadMode.Auto);
                gridviewSV.DataSource = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xảy ra khi đọc file: " + ex.ToString());
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                dataSet.WriteXml("../../SinhVien.xml", XmlWriteMode.IgnoreSchema);
                MessageBox.Show("Ghi tệp thành công!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xảy ra khi ghi file: " + ex.ToString());
                MessageBox.Show("Ghi tệp không thành công, hãy thử lại!", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (gridviewSV.ReadOnly)
            {
                EditInterface = true;
                MessageBox.Show("Chế độ chỉnh sửa đã mở vui lòng chỉnh sửa thông tin của sinh viên ở gridview", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                EditInterface = false;
        }
        //Button thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            tbTim.Enabled = false;

            //cac button an 
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnTim.Enabled = false;
            btnWrite.Enabled = false;
            btnRead.Enabled = false;
            btnThoat.Enabled = false;

            if (dataSet == null || EmptyTextBox)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin vào textbox sau đó nhấn nút [Thêm] để thêm sinh viên mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            else
            {
                dataSet.Tables[0].Rows.Add(tbMa.Text, tbTen.Text, tbLop.Text, tbNoisinh.Text);
                EmptyTextBox = true;
                btnTim.Enabled = true;

                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                btnWrite.Enabled = true;
                btnRead.Enabled = true;
                btnThoat.Enabled = true;
                MessageBox.Show("Đã thêm thành công sinh viên mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        //nut xoa
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (gridviewSV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Hãy chọn dòng cần xóa!");
                return;
            }
            DialogResult = MessageBox.Show("Bạn có chắc muốn xóa dòng này hay không!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.OK)
            {
                dataSet.Tables[0].Rows.RemoveAt(gridviewSV.SelectedRows[0].Index);
            }
        }

        private void ThongSinhVien_Load(object sender, EventArgs e)
        {
            EditInterface = true;
            btnSua.Enabled = false;
            btnRead.Enabled = true;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            tbTim.Enabled = true;

            string searchValue = tbTim.Text;
            if (tbTim.Text.ToString() != "")
            {


                gridviewSV.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                try
                {
                    foreach (DataGridViewRow row in gridviewSV.Rows)
                    {
                        if (row.Cells[0].Value.ToString().Equals(searchValue))
                        {
                            MessageBox.Show("Đã tìm thấy sinh viên có mã sinh viên " + tbTim.Text + " mà bạn đang cần tìm!", "THÔNG BÁO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            /**********************/
                            //mo lai cac texbox khong can thiet
                            tbMa.Enabled = true;
                            tbLop.Enabled = true;
                            tbTen.Enabled = true;
                            tbNoisinh.Enabled = true;
                            // mo lai cac button khong can thiet
                            btnSua.Enabled = true;
                            btnXoa.Enabled = true;
                            btnThem.Enabled = true;
                            btnWrite.Enabled = true;
                            btnRead.Enabled = true;
                            btnThoat.Enabled = true;
                            tbTim.Focus();
                            /**********************/
                            row.Selected = true;
                            break;
                        }
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                    

                }
            }
            if (tbTim.Text == "")
            {
                MessageBox.Show("Không được để trống ô tìm kiếm, Vui lòng điền vào mã sinh viên cần tìm vào ô [Tìm Kiếm MSV] và thử nhấn nút tìm kiếm lại!", "THÔNG BÁO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbTim.Focus();
                /**********************/
                //tat cac texbox khong can thiet
                tbMa.Enabled = false;
                tbLop.Enabled = false;
                tbTen.Enabled = false;
                tbNoisinh.Enabled = false;
                // tat cac button khong can thiet
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnThem.Enabled = false;
                btnWrite.Enabled = false;
                btnRead.Enabled = false;
                btnThoat.Enabled = false;
                /**********************/
            }

        }
    }
}
