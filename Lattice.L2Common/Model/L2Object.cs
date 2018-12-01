using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace Lattice.L2Common.Model
{
    public abstract class L2Object
    {
        private Vector3 _position = new Vector3(147465, 13559, -1151);

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [NotMapped]
        public int ObjectId { get; set; }

        // Position in 3D space
        public float X
        {
            get { return Position.X; }
            set { Position = new Vector3(value, Position.Y, Position.Z); }
        }

        public float Y
        {
            get { return Position.Y; }
            set { Position = new Vector3(Position.X, value, Position.Z); }
        }

        public float Z
        {
            get { return Position.Z; }
            set { Position = new Vector3(Position.X, Position.Y, value); }
        }

        public delegate void PositionChangeEvent(object sender, Vector3 position, Vector3 oldPosition);
        public event PositionChangeEvent PositionChanged;

        [NotMapped]
        public Vector3 Position
        {
            get { return _position; }
            set
            {
                Vector3 old = _position;
                _position = value;
                PositionChanged?.Invoke(this, value, old);
            }
        }
    }
}
