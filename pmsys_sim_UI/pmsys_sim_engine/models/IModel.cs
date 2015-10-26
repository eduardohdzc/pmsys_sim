using System;

namespace pmsys_sim_engine.models
{
    public interface IModel
    {
        string TableName { get; }
        string TablePK { get; }
        Int32? Id { get; set; }
    }
}