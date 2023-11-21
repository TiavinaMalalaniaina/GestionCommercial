using sales_departements.Models;
using Microsoft.EntityFrameworkCore;

namespace sales_departements.Context;

public partial class SalesDepartementsContext : DbContext
{
    public SalesDepartementsContext()
    {
    }

    public SalesDepartementsContext(DbContextOptions<SalesDepartementsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Proforma> Proformas { get; set; }

    public virtual DbSet<ProformaDetail> ProformaDetails { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestDetail> RequestDetails { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierProduct> SupplierProducts { get; set; }
    public virtual DbSet<VProforma> VProformas { get; set; }
    public virtual DbSet<VProductNecessary> VProductNecessarys { get; set; }
    public virtual DbSet<Session> Sessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=sales_departement;Username=postgres;Password=malalaniaina");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("session");

            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("value");

           
        });
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("department_pkey");

            entity.ToTable("department");

            entity.HasIndex(e => e.DepartmentName, "department_department_name_key").IsUnique();

            entity.Property(e => e.DepartmentId)
                .HasDefaultValueSql("custom_seq('DEP'::character varying, 'departement_seq'::character varying, 5)")
                .HasColumnType("character varying")
                .HasColumnName("department_id");
            entity.Property(e => e.DepartmentHeadId)
                .HasColumnType("character varying")
                .HasColumnName("department_head_id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .HasColumnName("department_name");

            entity.HasOne(d => d.DepartmentHead).WithMany(p => p.Departments)
                .HasForeignKey(d => d.DepartmentHeadId)
                .HasConstraintName("department_department_head_id_fkey");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.HasIndex(e => e.Email, "person_email_key").IsUnique();

            entity.HasIndex(e => e.PersonId, "employee_person_id_key").IsUnique();
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");

            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");

            entity.Property(e => e.EmployeeId)
                .HasDefaultValueSql("custom_seq('EMP'::character varying, 'employee_seq'::character varying, 5)")
                .HasColumnType("character varying")
                .HasColumnName("employee_id");
            entity.Property(e => e.DepartmentId)
                .HasColumnType("character varying")
                .HasColumnName("department_id");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(100)
                .HasColumnName("job_title");
            entity.Property(e => e.PersonId)
                .HasColumnType("character varying")
                .HasColumnName("person_id");
            entity.Property(e => e.Salary)
                .HasPrecision(10, 2)
                .HasColumnName("salary");
            entity.Property(e => e.Daf)
                .HasColumnName("daf");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("employee_department_id_fkey");

            entity.HasOne(d => d.Person).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.PersonId)
                .HasConstraintName("employee_person_id_fkey");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("person_pkey");

            entity.ToTable("person");

            entity.Property(e => e.PersonId)
                .HasDefaultValueSql("custom_seq('PER'::character varying, 'person_seq'::character varying, 5)")
                .HasColumnType("character varying")
                .HasColumnName("person_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("product_pkey");

            entity.ToTable("product");

            entity.HasIndex(e => e.ProductName, "product_product_name_key").IsUnique();

            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("custom_seq('PRO'::character varying, 'product_seq'::character varying, 5)")
                .HasColumnType("character varying")
                .HasColumnName("product_id");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("product_name");
        });

        modelBuilder.Entity<Proforma>(entity =>
        {
            entity.HasKey(e => e.ProformaId).HasName("proforma_pkey");

            entity.ToTable("proforma");

            entity.Property(e => e.ProformaId)
                .HasDefaultValueSql("custom_seq('PRO'::character varying, 'proforma_seq'::character varying, 5)")
                .HasColumnType("character varying")
                .HasColumnName("proforma_id");
            entity.Property(e => e.DueDate)
                .HasColumnName("due_date");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.SupplierId)
                .HasColumnType("character varying")
                .HasColumnName("supplier_id");

            // entity.HasOne(d => d.Supplier).WithMany(p => p.Proformas)
            //     .HasForeignKey(d => d.SupplierId)
            //     .HasConstraintName("proforma_supplier_id_fkey");
        });

        modelBuilder.Entity<ProformaDetail>(entity =>
        {
            entity.HasKey(e => e.ProformaDetailsId).HasName("proforma_details_pkey");

            entity.ToTable("proforma_details");

            entity.Property(e => e.ProformaDetailsId)
                .HasDefaultValueSql("custom_seq('PRD'::character varying, 'proforma_details_seq'::character varying, 5)")
                .HasColumnType("character varying")
                .HasColumnName("proforma_details_id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProductId)
                .HasColumnType("character varying")
                .HasColumnName("product_id");
            entity.Property(e => e.ProformaId)
                .HasColumnType("character varying")
                .HasColumnName("proforma_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            // entity.HasOne(d => d.Product).WithMany(p => p.ProformaDetails)
            //     .HasForeignKey(d => d.ProductId)
            //     .HasConstraintName("proforma_details_product_id_fkey");

            // entity.HasOne(d => d.Proforma).WithMany(p => p.ProformaDetails)
            //     .HasForeignKey(d => d.ProformaId)
            //     .HasConstraintName("proforma_details_proforma_id_fkey");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderId).HasName("purchase_order_pkey");

            entity.ToTable("purchase_order");

            entity.Property(e => e.PurchaseOrderId)
                .HasDefaultValueSql("custom_seq('PUR'::character varying, 'purchase_order_seq'::character varying, 5)")
                .HasColumnType("character varying")
                .HasColumnName("purchase_order_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DeliveryDays)
                .HasColumnName("delivery_days")
                .HasDefaultValueSql("30");
            entity.Property(e => e.SupplierId)
                .HasColumnType("character varying")
                .HasColumnName("supplier_id");
            entity.Property(e => e.Validation)
                .HasDefaultValueSql("10")
                .HasColumnName("validation");

            // entity.HasOne(d => d.Supplier).WithMany(p => p.PurchaseOrders)
            //     .HasForeignKey(d => d.SupplierId)
            //     .HasConstraintName("purchase_order_supplier_id_fkey");
        });

        modelBuilder.Entity<PurchaseOrderDetail>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderDetailsId).HasName("purchase_order_details_pkey");

            entity.ToTable("purchase_order_details");

            entity.Property(e => e.PurchaseOrderDetailsId)
                .HasDefaultValueSql("custom_seq('PUD'::character varying, 'purchase_order_details_seq'::character varying, 5)")
                .HasColumnType("character varying")
                .HasColumnName("purchase_order_details_id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProductId)
                .HasColumnType("character varying")
                .HasColumnName("product_id");
            entity.Property(e => e.PurchaseOrderId)
                .HasColumnType("character varying")
                .HasColumnName("purchase_order_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            // entity.HasOne(d => d.Product).WithMany(p => p.PurchaseOrderDetails)
            //     .HasForeignKey(d => d.ProductId)
            //     .HasConstraintName("purchase_order_details_product_id_fkey");
        });
        modelBuilder.Entity<VProforma>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("v_proforma_moins_disant");

            entity.Property(e => e.ProformaDetailsId)
                .HasColumnType("character varying")
                .HasColumnName("proforma_details_id");
            entity.Property(e => e.ProformaId)
                .HasColumnType("character varying")
                .HasColumnName("proforma_id");
            entity.Property(e => e.ProductId)
                .HasColumnType("character varying")
                .HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasColumnName("quantity");
            entity.Property(e => e.Price)
                .HasColumnName("price");
            entity.Property(e => e.SupplierId)
                .HasColumnType("character varying")
                .HasColumnName("supplier_id");
        });

        modelBuilder.Entity<VProductNecessary>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("v_product_necessary");

            entity.Property(e => e.ProductId)
                .HasColumnType("character varying")
                .HasColumnName("product_id");
            entity.Property(e => e.ProductName)
                .HasColumnType("character varying")
                .HasColumnName("product_name");
            entity.Property(e => e.Quantity)
                .HasColumnName("quantity");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("request_pkey");

            entity.ToTable("request");

            entity.Property(e => e.RequestId)
                .HasDefaultValueSql("custom_seq('REQ'::character varying, 'request_seq'::character varying, 5)")
                .HasColumnType("character varying")
                .HasColumnName("request_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DepartmentId)
                .HasColumnType("character varying")
                .HasColumnName("department_id");
            entity.Property(e => e.EmployeeId)
                .HasColumnType("character varying")
                .HasColumnName("employee_id");
            entity.Property(e => e.IsValidated)
                .HasDefaultValueSql("false")
                .HasColumnName("is_validated");

            // entity.HasOne(r => r.Department)
            //     .WithMany(p => p.Requests)  // Assuming there's a navigation property in the Department class pointing back to Requests
            //     .HasForeignKey(r => r.DepartmentId)
            //     .HasConstraintName("request_department_id_fkey");
        });

        modelBuilder.Entity<RequestDetail>(entity =>
        {
            entity.HasKey(e => e.RequestDetailsId).HasName("request_details_pkey");

            entity.ToTable("request_details");

            entity.Property(e => e.RequestDetailsId)
                .HasDefaultValueSql("custom_seq('RED'::character varying, 'request_details_seq'::character varying, 5)")
                .HasColumnType("character varying")
                .HasColumnName("request_details_id");
            entity.Property(e => e.ProductId)
                .HasColumnType("character varying")
                .HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.IsValidated).HasColumnName("is_validated");
            entity.Property(e => e.Treated).HasColumnName("treated");
            entity.Property(e => e.RequestId)
                .HasColumnType("character varying")
                .HasColumnName("request_id");
            entity.Property(e => e.DepartmentName).HasColumnName("departement_name");

            // entity.HasOne(d => d.Product).WithMany(p => p.RequestDetails)
            //     .HasForeignKey(d => d.ProductId)
            //     .HasConstraintName("request_details_product_id_fkey");

            // entity.HasOne(d => d.Request).WithMany(p => p.RequestDetails)
            //     .HasForeignKey(d => d.RequestId)
            //     .HasConstraintName("request_details_request_id_fkey");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("supplier_pkey");

            entity.ToTable("supplier");

            entity.HasIndex(e => e.Name, "supplier_name_key").IsUnique();

            entity.Property(e => e.SupplierId)
                .HasDefaultValueSql("custom_seq('SUP'::character varying, 'supplier_seq'::character varying, 5)")
                .HasColumnType("character varying")
                .HasColumnName("supplier_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(100)
                .HasColumnName("contact_email");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(20)
                .HasColumnName("contact_phone");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<SupplierProduct>(entity =>
        {
            entity.HasKey(e => e.SupplierProductId).HasName("supplier_product_pkey");

            entity.ToTable("supplier_product");

            entity.Property(e => e.SupplierProductId)
                .HasDefaultValueSql("custom_seq('SPR'::character varying, 'supplier_product_seq'::character varying, 5)")
                .HasColumnType("character varying")
                .HasColumnName("supplier_product_id");
            entity.Property(e => e.ProductId)
                .HasColumnType("character varying")
                .HasColumnName("product_id");
            entity.Property(e => e.SupplierId)
                .HasColumnType("character varying")
                .HasColumnName("supplier_id");

            // entity.HasOne(d => d.Product).WithMany(p => p.SupplierProducts)
            //     .HasForeignKey(d => d.ProductId)
            //     .HasConstraintName("supplier_product_product_id_fkey");

            // entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierProducts)
            //     .HasForeignKey(d => d.SupplierId)
            //     .HasConstraintName("supplier_product_supplier_id_fkey");
        });
        modelBuilder.HasSequence("departement_seq");
        modelBuilder.HasSequence("employee_seq");
        modelBuilder.HasSequence("person_seq");
        modelBuilder.HasSequence("product_seq");
        modelBuilder.HasSequence("proforma_details_seq");
        modelBuilder.HasSequence("proforma_seq");
        modelBuilder.HasSequence("purchase_order_details_seq");
        modelBuilder.HasSequence("purchase_order_seq");
        modelBuilder.HasSequence("request_details_seq");
        modelBuilder.HasSequence("request_seq");
        modelBuilder.HasSequence("supplier_product_seq");
        modelBuilder.HasSequence("supplier_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
