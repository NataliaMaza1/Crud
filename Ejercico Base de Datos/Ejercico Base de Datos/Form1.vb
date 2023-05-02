
Imports MySql.Data.MySqlClient
Public Class Form1
    Dim bandera As Integer


    Sub cargardatagrid()

        Dim cString As String
        cString = "server=localhost;user=root;database=biblioteca;port=3306;password=CVO2023;"
        'Crear Conexión
        Dim conn As New MySqlConnection(cString)
        Try
            'Abrir la Conexión
            conn.Open()
            'Crear cadena de consulta
            Dim SQuerry As String
            SQuerry = "Select * FROM autor ;"
            'Consulta Sql
            'Se crea el Data Adapter
            Dim da As New MySqlDataAdapter(SQuerry, conn)
            'Crear un DataTable
            Dim dt As New DataTable
            'Llenamos el Data Table con el Adaptador
            da.Fill(dt)
            'Llenamos el Data Table con el DataTable
            DataGridView1.DataSource = dt
            DataGridView1.Refresh()
            'Cerar Conexión
            conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub



    Dim nombre, apellido, pais As String

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim idautor As Integer
        Dim cString As String

        idautor = DataGridView1.CurrentRow.Cells(0).Value
        Dim Squery As String
        Squery = "select""From autor where idautor" & idautor & ";"
        cString = "server=localhost;user=root;database=biblioteca;port=3306;
           password=CVO2023;"
        bandera = idautor
        Try
            Dim conn As New MySqlConnection(cString)
            Dim da As New MySqlDataAdapter(Squery, conn)
            Dim dt As New DataTable()
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim fila As DataRow = dt.Rows(0)
                TextBox1.Text = fila("nombre").ToString()
                TextBox2.Text = fila("appelido").ToString()
                TextBox3.Text = fila("pais").ToString()
                DateTimePicker1.Value = Convert.ToDateTime(fila("fecha_nac")).Date
                DateTimePicker2.Value = Convert.ToDateTime(fila("fecha_muerte")).Date
            Else
                MessageBox.Show("No existe el autor")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try




    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cargardatagrid()

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim fecha_nac, fecha_muerte As Date
        Dim SQUERY As String
        nombre = TextBox1.Text
        apellido = TextBox2.Text
        pais = TextBox3.Text
        fecha_nac = DateTimePicker1.Value.Date
        fecha_muerte = DateTimePicker2.Value.Date
        Dim cString As String
        cString = "server=localhost;user=root;database=biblioteca;port=3306;
           password=CVO2023;"
        Dim conn As New MySqlConnection(cString)
        Try
            conn.Open()
            Dim cm As New MySqlCommand
            SQUERY = "Insert Into autor(nombre,apellidos,fecha_nac,fecha_muerte,pais) Values(@nombre,@apellido,@fecha_nac,@fecha_muerte,@pais);"
            cm.Connection() = conn
            cm.CommandText() = SQUERY
            cm.Parameters.AddWithValue("@nombre", nombre)
            cm.Parameters.AddWithValue("@apellido", apellido)
            cm.Parameters.AddWithValue("@fecha_nac", fecha_nac)
            cm.Parameters.AddWithValue("@fecha_muerte", fecha_muerte)
            cm.Parameters.AddWithValue("@pais", pais)
            cm.ExecuteNonQuery()
            MessageBox.Show("Guardado con exito ")
            cargardatagrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try





    End Sub


End Class

