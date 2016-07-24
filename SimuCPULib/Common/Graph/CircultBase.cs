using System;
using System.Collections.Generic;
using SimuCPULib.Common.Base;
using SimuCPULib.Common.Element;

namespace SimuCPULib.Common.Graph
{
	/// <summary>
	/// 电路基类
	/// </summary>
	/// <typeparam name="_STATUS"></typeparam>
	/// <typeparam name="_NODE"></typeparam>
	public class CircultBase<_STATUS, _NODE>
		where _STATUS : Status, new()
		where _NODE : Node<_STATUS>, new()
	{
		private readonly Dictionary<Guid, _NODE> _nodes = new Dictionary<Guid, _NODE>();
        private readonly Dictionary<int, Guid> _nodesTable = new Dictionary<int, Guid>();

        protected Dictionary<Guid, _NODE> Nodes => _nodes;

	    protected _NODE CreateNode()
        {
			var node = new _NODE();
            _nodesTable.Add(_nodes.Count, node.Id);
			_nodes.Add(node.Id, node);
            return node;
		}

	    public Guid GetGuidById(int id)
	    {
	        return _nodesTable[id];
	    }
	}
}
