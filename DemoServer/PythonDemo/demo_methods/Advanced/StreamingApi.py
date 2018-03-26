from demo.holder import DocumentStoreHolder
from demo.entities import Order

class StreamingApi(object):
	@staticmethod
	def streaming_api:
		with DocumentStoreHolder.get_store().open_session() as session:
            query = session.query(object_type=Order, index_name="OrderByCompanyAndCountry")
			count = 0
            results = session.advanced.stream(query)
            for result in results:
				# do something with this
				order = result.get("document", None)
                count += 1