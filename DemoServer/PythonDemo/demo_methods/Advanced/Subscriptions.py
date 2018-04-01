from demo.holder import DocumentStoreHolder
from pyravendb.subscriptions.data import SubscriptionCreationOptions, SubscriptionWorkerOptions
from pyravendb.subscriptions.data import SubscriptionOpeningStrategy
from pyravendb.custom_exceptions.exceptions import SubscriptionClosedException


class Subscriptions(object):

    @staticmethod
    def subscriptions(subscriptionName=None):
        companies = []
        with DocumentStoreHolder.get_store() as store:
            if not subscriptionName:
                subscription_creation_options = SubscriptionCreationOptions("From Companies")
                subscriptionName = store.subscriptions.create(subscription_creation_options)

            worker_options = SubscriptionWorkerOptions(subscriptionName, strategy=SubscriptionOpeningStrategy.take_over,
                                                       close_when_no_docs_left=True)
            with store.subscriptions.get_subscription_worker(worker_options) as subscription_worker:
                try:
                    subscription_worker.run(
                        partial(lambda batch, x=companies: x.extend([item.raw_result for item in batch.items]))).join()
                except SubscriptionClosedException:
                    # That's expected
                    pass

        return companies