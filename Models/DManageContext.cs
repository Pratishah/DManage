using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DManage.Models
{
    public partial class DManageContext : DbContext
    {
        public DManageContext()
        {
        }

        public DManageContext(DbContextOptions<DManageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerTable> CustomerTables { get; set; }
        public virtual DbSet<Node> Nodes { get; set; }
        public virtual DbSet<NodeEdge> NodeEdges { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderQuantity> OrderQuantities { get; set; }
        public virtual DbSet<Pallate> Pallates { get; set; }
        public virtual DbSet<ProductInventory> ProductInventories { get; set; }
        public virtual DbSet<ProductMaster> ProductMasters { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=DManage;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CustomerTable>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("CustomerTable");

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("CustomerID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Node>(entity =>
            {
                entity.ToTable("Node");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.CratedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Createdby)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDatetime).HasColumnType("datetime");

                entity.Property(e => e.Modifiedby)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NodeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("zone");
            });

            modelBuilder.Entity<NodeEdge>(entity =>
            {
                entity.HasKey(e => e.EdgeId);

                entity.ToTable("NodeEdge");

                entity.Property(e => e.EdgeId)
                    .ValueGeneratedNever()
                    .HasColumnName("EdgeID");

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistanceM).HasColumnName("Distance(m)");

                entity.Property(e => e.EndNodeId).HasColumnName("EndNodeID");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.StartNodeId).HasColumnName("StartNodeID");

                entity.HasOne(d => d.EndNode)
                    .WithMany(p => p.NodeEdgeEndNodes)
                    .HasForeignKey(d => d.EndNodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NodeEdge_Node1");

                entity.HasOne(d => d.StartNode)
                    .WithMany(p => p.NodeEdgeStartNodes)
                    .HasForeignKey(d => d.StartNodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NodeEdge_Node");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_CustomerTable");

                entity.HasOne(d => d.OrderNavigation)
                    .WithOne(p => p.Order)
                    .HasForeignKey<Order>(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_OrderQuantity");
            });

            modelBuilder.Entity<OrderQuantity>(entity =>
            {
                entity.ToTable("OrderQuantity");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
            });

            modelBuilder.Entity<Pallate>(entity =>
            {
                entity.ToTable("Pallate");

                entity.Property(e => e.PallateId)
                    .ValueGeneratedNever()
                    .HasColumnName("PallateID");

                entity.Property(e => e.CreateDatetime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDatetime).HasColumnType("datetime");

                entity.Property(e => e.NodeId).HasColumnName("NodeID");

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");

                entity.HasOne(d => d.Node)
                    .WithMany(p => p.Pallates)
                    .HasForeignKey(d => d.NodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pallate_Node");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Pallates)
                    .HasForeignKey(d => d.ProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pallate_Pallate");
            });

            modelBuilder.Entity<ProductInventory>(entity =>
            {
                entity.ToTable("ProductInventory");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDatetime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDatetime).HasColumnType("datetime");

                entity.Property(e => e.PallateId).HasColumnName("PallateID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Pallate)
                    .WithMany(p => p.ProductInventories)
                    .HasForeignKey(d => d.PallateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInventory_Pallate");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInventories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInventory_ProductMaster");
            });

            modelBuilder.Entity<ProductMaster>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("ProductMaster");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductID");

                entity.Property(e => e.Lot)
                    .IsUnicode(false)
                    .HasColumnName("lot");

                entity.Property(e => e.ProductDescription).IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.ProductMasters)
                    .HasForeignKey(d => d.ProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductMaster_ProductType");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType");

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
