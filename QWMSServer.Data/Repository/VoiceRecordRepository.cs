using QWMSServer.Data.Infrastructures;
using QWMSServer.Model.DatabaseModels;

namespace QWMSServer.Data.Repository
{
	public class VoiceRecordRepository : AsyncRepository<VoiceRecord>, IVoiceRecordRepository
	{
		public VoiceRecordRepository(IDBContext dbContext) : base(dbContext)
		{
		}
	}
}
