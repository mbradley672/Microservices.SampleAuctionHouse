import {Auction, PagedResult} from "@/types";
import {create} from "zustand";

type State = {
    auctions: Auction[]
    totalCount: number
    pageCount: number
}

type Actions = {
    setData: (data: PagedResult<Auction>) => void
    seCurrentPrice: (auctionId: string, price: number) => void

}

const initialState: State = {
    auctions: [],
    totalCount: 0,
    pageCount: 0
}

export const useAuctionStore = create<State & Actions>((set) => ({
    ...initialState,
    setData: (data) => {
        set(() => ({
            auctions: data.results,
            totalCount: data.totalCount,
            pageCount: data.pageCount

        }))
    },
    seCurrentPrice: (auctionId, price) => {
        set((state) => ({
            auctions: state.auctions.map((auction) => auction.id === auctionId ?
                {...auction, currentHighBid: price} : auction)
        }))
    }
}))