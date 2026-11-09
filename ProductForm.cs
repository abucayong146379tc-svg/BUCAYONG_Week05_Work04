namespace BUCAYONG_Week05_Work04;

public class ProductForm : Form
{
    private readonly TextBox barcodeText = new();
    private readonly TextBox nameText = new();
    private readonly TextBox descriptionText = new();
    private readonly ComboBox categoryCombo = new();
    private readonly RadioButton localRadio = new();
    private readonly RadioButton abroadRadio = new();
    private readonly CheckBox activeCheck = new();
    private readonly TextBox costText = new();
    private readonly TextBox priceText = new();
    private readonly TextBox markupText = new();
    private readonly Label barcodeMessage = new();

    public ProductForm()
    {
        Text = "Product Form";
        ClientSize = new Size(430, 510);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Font = new Font("Segoe UI", 9);
        BackColor = Color.WhiteSmoke;

        BuildForm();
        MakeBarcode();
    }

    private void BuildForm()
    {
        // Barcode
        AddLabel("Barcode", 18, 22);
        AddTextBox(barcodeText, 115, 18, 285);
        barcodeText.ReadOnly = true;
        barcodeText.BackColor = Color.White;
        barcodeMessage.Text = "Barcode is available.";
        barcodeMessage.Font = new Font("Segoe UI", 8, FontStyle.Italic);
        barcodeMessage.ForeColor = Color.Gray;
        barcodeMessage.Location = new Point(116, 43);
        barcodeMessage.AutoSize = true;
        Controls.Add(barcodeMessage);

        // Name
        AddLabel("Name", 18, 72);
        AddTextBox(nameText, 115, 68, 285);

        // Description
        AddLabel("Description", 18, 108);
        AddTextBox(descriptionText, 115, 104, 285, 72);
        descriptionText.Multiline = true;
        descriptionText.ScrollBars = ScrollBars.Vertical;

        // Category
        AddLabel("Category", 18, 188);
        categoryCombo.Location = new Point(115, 184);
        categoryCombo.Size = new Size(285, 26);
        categoryCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        categoryCombo.Items.AddRange(new object[]
        {
            "Select category", "Electronics", "Food", "Clothing",
            "Home & Living", "School Supplies", "Others"
        });
        categoryCombo.SelectedIndex = 0;
        Controls.Add(categoryCombo);

        // Manufactured / Origin group
        GroupBox originGroup = new()
        {
            Text = "Manufactured",
            Location = new Point(115, 220),
            Size = new Size(285, 65)
        };

        localRadio.Text = "Local";
        localRadio.Location = new Point(20, 27);
        localRadio.AutoSize = true;
        localRadio.Checked = true;

        abroadRadio.Text = "Abroad";
        abroadRadio.Location = new Point(145, 27);
        abroadRadio.AutoSize = true;

        originGroup.Controls.Add(localRadio);
        originGroup.Controls.Add(abroadRadio);
        Controls.Add(originGroup);

        // Status
        activeCheck.Text = "Active";
        activeCheck.Location = new Point(115, 295);
        activeCheck.AutoSize = true;
        activeCheck.Checked = true;
        Controls.Add(activeCheck);

        // Cost, Price, Markup
        AddLabel("Cost", 18, 340);
        AddLabel("Price", 145, 340);
        AddLabel("Markup (%)", 275, 340);

        AddTextBox(costText, 18, 362, 112);
        AddTextBox(priceText, 145, 362, 112);
        AddTextBox(markupText, 275, 362, 125);
        markupText.ReadOnly = true;

        costText.KeyPress += NumberOnly;
        priceText.KeyPress += NumberOnly;
        costText.TextChanged += CalculateMarkup;
        priceText.TextChanged += CalculateMarkup;

        // Buttons
        Button cancelButton = MakeButton("Cancel", 18, 425);
        Button clearButton = MakeButton("Clear", 145, 425);
        Button saveButton = MakeButton("Save", 275, 425);

        cancelButton.Click += (sender, e) => Close();
        clearButton.Click += ClearForm;
        saveButton.Click += SaveProduct;

        Controls.Add(cancelButton);
        Controls.Add(clearButton);
        Controls.Add(saveButton);

        AcceptButton = saveButton;
        CancelButton = cancelButton;
    }

    private void AddLabel(string text, int x, int y)
    {
        Controls.Add(new Label
        {
            Text = text,
            Location = new Point(x, y),
            AutoSize = true
        });
    }

    private void AddTextBox(TextBox textBox, int x, int y, int width, int height = 25)
    {
        textBox.Location = new Point(x, y);
        textBox.Size = new Size(width, height);
        Controls.Add(textBox);
    }

    private Button MakeButton(string text, int x, int y)
    {
        return new Button
        {
            Text = text,
            Location = new Point(x, y),
            Size = new Size(110, 34)
        };
    }

    private void MakeBarcode()
    {
        barcodeText.Text = "PRD-" + DateTime.Now.ToString("yyyyMMddHHmmss");
    }

    private void NumberOnly(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) &&
            !char.IsDigit(e.KeyChar) &&
            e.KeyChar != '.')
        {
            e.Handled = true;
        }

        if (e.KeyChar == '.' && sender is TextBox box && box.Text.Contains('.'))
        {
            e.Handled = true;
        }
    }

    private void CalculateMarkup(object? sender, EventArgs e)
    {
        if (decimal.TryParse(costText.Text, out decimal cost) &&
            decimal.TryParse(priceText.Text, out decimal price) &&
            cost > 0)
        {
            decimal markup = ((price - cost) / cost) * 100;
            markupText.Text = markup.ToString("0.00");
        }
        else
        {
            markupText.Clear();
        }
    }

    private void SaveProduct(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(nameText.Text))
        {
            MessageBox.Show("Please enter the product name.", "Required Field",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            nameText.Focus();
            return;
        }

        if (categoryCombo.SelectedIndex == 0)
        {
            MessageBox.Show("Please select a category.", "Required Field",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            categoryCombo.Focus();
            return;
        }

        if (!decimal.TryParse(costText.Text, out decimal cost) || cost <= 0)
        {
            MessageBox.Show("Please enter a valid cost greater than zero.", "Invalid Cost",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            costText.Focus();
            return;
        }

        if (!decimal.TryParse(priceText.Text, out decimal price) || price <= 0)
        {
            MessageBox.Show("Please enter a valid price greater than zero.", "Invalid Price",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            priceText.Focus();
            return;
        }

        string origin = localRadio.Checked ? "Local" : "Abroad";
        string status = activeCheck.Checked ? "Active" : "Inactive";

        MessageBox.Show(
            "Product saved successfully!\n\n" +
            "Barcode: " + barcodeText.Text + "\n" +
            "Name: " + nameText.Text + "\n" +
            "Description: " + descriptionText.Text + "\n" +
            "Category: " + categoryCombo.Text + "\n" +
            "Manufactured: " + origin + "\n" +
            "Status: " + status + "\n" +
            "Cost: ₱" + cost.ToString("0.00") + "\n" +
            "Price: ₱" + price.ToString("0.00") + "\n" +
            "Markup: " + markupText.Text + "%",
            "Product Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ClearForm(object? sender, EventArgs e)
    {
        MakeBarcode();
        nameText.Clear();
        descriptionText.Clear();
        categoryCombo.SelectedIndex = 0;
        localRadio.Checked = true;
        activeCheck.Checked = true;
        costText.Clear();
        priceText.Clear();
        markupText.Clear();
        nameText.Focus();
    }
}
