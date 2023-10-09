import Image from 'next/image'
import Listings from "@/app/auctions/Listings";



export default function Home() {
  return (
      <div>
        <h3 className='text-3xl font-semibold'>
            <Listings />
        </h3>
      </div>
  )
}
