export type Auction = {
    createdAt: string
    updatedAt: string
    auctionEnd: string
    make: string
    seller: string
    winner?: any
    model: string
    color: string
    imageUrl: string
    status: string
    year: number
    mileage: number
    reservePrice: number
    soldAmount: number
    currentHighBid: number
    id: string
}

export type PagedResult<T> = {
    results: T[],
    pageCount: number,
    totalCount: number
}

export type SearchParams = {
    searchTerm: string | null;
    pageNumber: number | null;
    pageSize: number | null;
    seller: string | null;
    winner: string | null;
    orderBy: string | null;
    filterBy: string | null;
}